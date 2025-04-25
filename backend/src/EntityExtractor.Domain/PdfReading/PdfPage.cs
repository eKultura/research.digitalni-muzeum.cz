namespace eKultura.EntityExtractor.Domain.PdfReading;

/// <summary>
/// Represents a page in a PDF document
/// </summary>
/// <param name="Number">Number of the page</param>
/// <param name="Words">A list of words on the page</param>
/// <param name="NumberLabel">Number label on the page. Can be represented by a positive integer,
/// but also by a roman number, e.g. in case of a book preface.</param>
public record PdfPage(int Number, IList<string> Words, string? NumberLabel = null)
{
    private int _number;

    public int Number
    {
        get => _number;
        init => _number = value >= PdfReadingConstants.MinPdfPageNumber ? value 
            : throw new ArgumentException("PDF page has to be a positive integer.");
    }

    public string VirtualNumber { get; init; } = NumberLabel ?? Number.ToString();
}
