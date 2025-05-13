namespace eKultura.EntityExtractor.Contracts;

/// <summary>
/// Represents a PDF document
/// </summary>
/// <param name="DocumentId">Name of the document</param>
/// <param name="Document">Byte array containing the PDF document</param>
/// <param name="Project">Topic of the document</param>
/// <param name="Name">Name of the document for end-user display</param>
public record PdfDocument(string DocumentId, byte[] Document, string Project, string Name);