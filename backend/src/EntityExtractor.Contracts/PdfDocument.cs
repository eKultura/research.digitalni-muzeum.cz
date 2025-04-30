namespace eKultura.EntityExtractor.Contracts;

/// <summary>
/// Represents a PDF document
/// </summary>
/// <param name="Name">Name of the document</param>
/// <param name="Document">Byte array containing the PDF document</param>
/// <param name="Topic">Topic of the document</param>
public record PdfDocument(string Name, byte[] Document, string Topic);