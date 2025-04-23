namespace eKultura.EntityExtractor.Domain.PdfReading;

/// <summary>
/// Represents PDF document
/// </summary>
/// <param name="PageCount">Total number of pages in the document</param>
/// <param name="WordCount">Total number of words in the document</param>
/// <param name="Text">All document text</param>
public record PdfDocument(int PageCount, int WordCount, string Text);
