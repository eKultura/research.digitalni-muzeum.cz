# 📝 Architecture Decision Record (ADR)

## Title  
Storing File Metadata and Extracted Entities in MariaDB

## Status  
**Proposed**

## Context  
We need to store structured metadata extracted from PDF documents uploaded to `research.digitalni-muzeum.cz`.  
Each file is associated with a project (i.e., a folder), has a format and flags like OCR or public visibility, and will later be linked to a user.  
In addition to file-level metadata, the system must store **extracted entities** such as persons, locations, and years – including their **type**, **value**, and **location within the document** (e.g., page number).  
These records must be efficiently queryable, as the system is optimized for **read-heavy workloads**.

## Decision  
Use **MariaDB** and implement a normalized schema with the following tables:

- `research_files`: stores file-level metadata
- `research_entities`: stores extracted entities from each file
- `research_users`: stores user information -- NEXT PHASE
- `research_roles`: stores user role definitions -- NEXT PHASE

This design supports structured querying, future user/project associations, and clear separation of concerns.

## Alternatives Considered

| Option | Pros | Cons |
|--------|------|------|
| MariaDB with normalized tables | Compatible with existing stack, efficient indexing, clear structure | Requires joins and schema evolution for new needs |
| JSON document store (e.g., MongoDB) | Good for unstructured/nested data | Inconsistent with existing systems, harder for structured queries |
| CSV / flat file metadata | Easy initial setup | Poor scalability, hard to search, no relationships |

## Rationale  
MariaDB provides reliable and performant relational capabilities suited to the needs of this project:
- Efficient indexed queries (e.g., search all persons across all files)
- Data integrity via foreign keys
- Smooth integration with current tech stack

The schema reflects the reality of processing historical and scholarly documents: structured yet extensible data with multiple layers of metadata.

## Consequences  
The following schema will be created in MariaDB:

```sql
CREATE TABLE research_files (
  id INT AUTO_INCREMENT PRIMARY KEY,
  -- user_id INT, -- to be added later as FOREIGN KEY to research_users
  file_name VARCHAR(255) NOT NULL,
  project VARCHAR(255),
  -- project_id INT, -- to be added later as FOREIGN KEY
  format VARCHAR(50),
  ocr BOOLEAN DEFAULT TRUE,
  is_public BOOLEAN DEFAULT FALSE,
  uploaded_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE research_entities (
  id INT AUTO_INCREMENT PRIMARY KEY,
  file_id INT NOT NULL,
  entity_type ENUM('Person', 'Location', 'Year') NOT NULL,
  value VARCHAR(255) NOT NULL,
  page INT,
  -- confidence FLOAT, -- optional future field
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
