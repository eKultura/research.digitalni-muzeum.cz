# 📝 Architecture Decision Record (ADR)

## Title  
Selecting a Database for Storing Extracted Entities and File Metadata

## Status  
**Proposed**  
**Date:** 2025-05-12

## Context  
We aim to store metadata and NLP-extracted entities from PDF documents uploaded to `research.digitalni-muzeum.cz`.  
Each file is associated with a project (e.g., folder), has a format, and flags such as OCR completion and public visibility.  
Entities such as persons, locations, and years are extracted using OCR/NLP and must be stored with their **type**, **value**, and **location within the document** (typically page numbers, possibly multiple).

At this stage, requirements are still evolving, but we already know:
- The system will be **read-heavy**
- Data must be searchable and queryable (e.g., “Find all mentions of Napoleon in all documents of project X”)

### Notes:
- **OCR flag:** We include a `ocr` boolean field to indicate whether the file has already been processed for text recognition. This allows us to avoid redundant OCR runs and track processing status.
- **Entity page references:** Entities may occur on **multiple pages**, but for now we store a single page per record. Future design may include a join table or JSON field to support multiple occurrences per file.
- **Confidence score:** In the future, each entity may include a confidence value (0–1) that reflects the system’s certainty, especially useful for NLP-based extraction (e.g., “Paris” = location, confidence 0.9).
- **Original vs stored filename:** To ensure traceability and flexible storage, we plan to store both the uploaded filename and the internal/generated filename.

Future goals may include:
- Full-text search integration (e.g., ElasticSearch)
- Relationship analysis between entities (e.g., co-occurrence, graph-based queries)
- Visualization tools or timeline exports

## Decision  
Use **MariaDB** and implement a normalized schema with the following tables:

- `research_files`: stores file-level metadata
- `research_entities`: stores extracted entities from each file
- `research_users`: stores user information — **NEXT PHASE**
- `research_roles`: stores user role definitions — **NEXT PHASE**

This design supports structured querying, future user/project associations, and clear separation of concerns. It also builds on existing team experience with MariaDB.

## Alternatives Considered

| Option | Pros | Cons |
|--------|------|------|
| **MariaDB with normalized tables** | Familiar to the team, easy to index, clear relational structure, strong long-term maintainability | Requires more joins, complex schema evolution if structure changes |
| **JSON document store (e.g., MongoDB)** | Very flexible, schema-free design, allows storing file + entities in a single document; powerful for fast prototyping and full-text indexing; fits natural structure of "file → entities" | Requires more discipline to enforce consistency; relational queries are more complex or require aggregation pipelines |
| **Graph database (e.g., Neo4j)** | Best suited for analyzing relationships between entities, supports co-occurrence, timelines, or semantic webs | Overkill for current MVP; team has limited experience; complex for simple queries |
| **Flat file (CSV, JSON)** | Minimal setup, good for quick one-off processing or small datasets | Not queryable, no relationships, unsuitable for scaling or multi-user access |


## Rationale  
While MongoDB offers an attractive, flexible document model and aligns well with the nested nature of our data (e.g., a file containing an array of entities), we choose MariaDB for this MVP phase due to:

- Existing team proficiency and operational infrastructure
- Familiar and proven SQL tooling
- Easier enforcement of data integrity and referential relationships
- Strong indexing for read-heavy workloads with structured queries

This does not rule out the possibility of switching to, or augmenting with, a document-based or graph-based store in future phases — especially if relationships, fuzzy queries, or complex aggregations become core to the application.


## Consequences  
The following schema will be implemented in MariaDB:

```sql
CREATE TABLE research_files (
  id INT AUTO_INCREMENT PRIMARY KEY,
  -- user_id INT, -- to be added later as FOREIGN KEY to research_users
  original_file_name VARCHAR(255),
  stored_file_name VARCHAR(255),
  project VARCHAR(255),
  -- project_id INT, -- to be added later as FOREIGN KEY
  format VARCHAR(50),
  ocr BOOLEAN DEFAULT TRUE, -- indicates if OCR has been completed
  is_public BOOLEAN DEFAULT FALSE,
  uploaded_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE research_entities (
  id INT AUTO_INCREMENT PRIMARY KEY,
  file_id INT NOT NULL,
  entity_type ENUM('Person', 'Location', 'Year') NOT NULL,
  value VARCHAR(255) NOT NULL,
  page INT, -- for now, a single page per entity; may be extended later
  -- confidence FLOAT, -- optional: 0–1 score for NLP certainty
  -- context_snippet TEXT, -- optional future field
  FOREIGN KEY (file_id) REFERENCES research_files(id)
);

-- NEXT PHASE

CREATE TABLE research_roles (
  id INT AUTO_INCREMENT PRIMARY KEY,
  role_name VARCHAR(100) UNIQUE NOT NULL,
  description TEXT
);

CREATE TABLE research_users (
  id INT AUTO_INCREMENT PRIMARY KEY,
  email VARCHAR(255) UNIQUE NOT NULL,
  password_hash VARCHAR(255) NOT NULL,
  first_name VARCHAR(100),
  last_name VARCHAR(100),
  title_before VARCHAR(50),
  title_after VARCHAR(50),
  role_id INT,
  is_active BOOLEAN DEFAULT TRUE,
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  FOREIGN KEY (role_id) REFERENCES research_roles(id)
);


