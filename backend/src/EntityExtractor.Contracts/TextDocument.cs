namespace eKultura.EntityExtractor.Contracts;

/// <summary>
/// Represents a document containing text
/// </summary>
/// <param name="PageCount">Total number of pages in the document</param>
/// <param name="WordCount">Total number of words in the document</param>
/// <param name="Text">All document text</param>
public record TextDocument(int PageCount, int WordCount, string Text);