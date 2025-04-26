namespace eKultura.EntityExtractor.Contracts;

/// <summary>
/// Represents a PDF document
/// </summary>
/// <param name="Name">Name of the document</param>
/// <param name="DocumentStream">Stream containing the PDF document</param>
/// <param name="Topic">Topic of the document</param>
public record PdfDocument(string Name, MemoryStream DocumentStream, string Topic) : IDisposable
{
    public void Dispose()
    {
        DocumentStream?.Dispose();
    }
}
