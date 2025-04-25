namespace eKultura.EntityExtractor.Domain.PdfReading;

public interface IPdfTextReader
{
    /// <summary>
    /// Reads text from the PDF file.
    /// </summary>
    /// <param name="stream">Memory stream containing PDF file</param>
    /// <returns>PDF file with its text content stored in string.</returns>
    /// <exception cref="InvalidOperationException">If the memory stream does not contain PDF file</exception>
    /// <exception cref="InvalidOperationException">If the PDF file is encrypted</exception>
    Task<PdfDocument> ReadTextAsync(MemoryStream stream);
}