# 📚 Project: Digital research of historical documents	

## 🚀 Project Phases

### 1. Upload PDF to Folder 
- Web form to upload one or more PDF files.
- User can choose an existing folder or create a new project folder.
- Save PDFs into the selected folder.

### 2. Save Metadata 
- Save information for each uploaded file:
  - file name
  - upload date and time
  - project (folder name)

### 3. Display Uploaded Documents
- Show a list of uploaded documents inside the selected project.
- Allow downloading the PDFs.
- (Optional) Show PDF preview using iframe or PDF.js.

### 4. NLP Text Analysis (MAIN GOAL)

- Run basic NLP analysis on each uploaded document:
  - extract entities: persons, places, organizations, dates
  - save position information (for example: page number, paragraph number)
- (Later) Add:
  - indexing of processed texts for faster searching
  - advanced filtering (find entities near certain words, by context)

#### NLP tools we are considering:

| Tool | Pros | Cons |
|:---|:---|:---|
| **spaCy (Python)** | Very fast, easy to use, many pre-trained models | Needs Python server, external API if used with C# |
| **Stanford CoreNLP** | Extremely powerful, supports deep linguistic analysis | Runs on Java, needs wrapper (Stanford.NLP.NET) |
| **ML.NET (C#)** | Integrated with .NET, easy for C# developers | Limited out-of-the-box models, training needed |
| **Azure Cognitive Services (Cloud API)** | Very easy to start, OCR + NLP together | Requires internet connection, API costs money |

- For MVP we prefer an easy, ready-to-use tool (spaCy or Azure).
- Later we can switch to a more advanced tool (Stanford CoreNLP) if needed.



---

## 🕓 Next Phases (Later, after MVP works)

### 5. OCR Processing (Later)
- Add OCR to read text from scanned images (if PDFs are not readable).
- Save extracted text for further NLP analysis.

### 6. Upload Multiple Documents at Once (Later)
- Enable drag & drop or multi-file upload.

### 7. User Login and Authentication (Later)
- Users must log in to upload and manage documents.

### 8. Project Management (Later)
- Create, edit, delete projects.
- Assign documents to projects.

---
