using eKultura.EntityExtractor.Domain.PdfReading;

namespace eKultura.EntityExtractor.Contracts;

/// <summary>
/// Represents a document containing text
/// </summary>
/// <param name="Name">Name of the text document</param>
/// <param name="Pages">All document pages</param>
/// <param name="Topic">Document's topic</param>
public record TextDocument(string Name, IReadOnlyList<TextDocumentPage> Pages, string Topic);