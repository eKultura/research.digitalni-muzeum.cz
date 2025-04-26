using eKultura.EntityExtractor.Contracts;

namespace eKultura.EntityExtractor.Domain.PdfReading;

/// <summary>
/// Represents a page in a text document
/// </summary>
public record TextDocumentPage
{
    private string? _numberLabel = null;

    public int Number { get; init; }
    public string Text { get; init; }
    public string NumberLabel => _numberLabel ?? Number.ToString();

    /// <summary>
    /// Ctor
    /// <param name="Number">Number of the page</param>
    /// <param name="Text">A text on the page</param>
    /// <param name="NumberLabel">Number label on the page. Can be represented by a positive integer,
    /// but also by a roman number, e.g. in case of a book preface.</param>
    public TextDocumentPage(int number, string text, string? numberLabel = null)
    {
        if (number < DocumentReadingConstants.MinPdfPageNumber)
        {
            throw new ArgumentException("PDF page has to be a positive integer.");
        }

        Number = number;
        Text = text;
        _numberLabel = numberLabel;
    }
}
