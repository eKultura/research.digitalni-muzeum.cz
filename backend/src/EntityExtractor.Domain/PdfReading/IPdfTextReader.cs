using eKultura.EntityExtractor.Contracts;

namespace eKultura.EntityExtractor.Domain.PdfReading;

public interface IPdfTextReader
{
    /// <summary>
    /// Reads text from the PDF file.
    /// </summary>
    /// <param name="pdfDocument">PDF document</param>
    /// <returns>PDF file with its text content stored in string.</returns>
    /// <exception cref="InvalidOperationException">If the document provided is not a PDF file</exception>
    /// <exception cref="InvalidOperationException">If the PDF file is encrypted</exception>
    Task<TextDocument> ReadTextAsync(PdfDocument pdfDocument);
}