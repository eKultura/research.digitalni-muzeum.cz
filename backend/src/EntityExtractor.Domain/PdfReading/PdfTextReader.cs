using eKultura.EntityExtractor.Contracts;
using Microsoft.Extensions.Logging;
using PdfSharpCore.Pdf.IO;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Exceptions;
using PdfPigDocument = UglyToad.PdfPig.PdfDocument;

namespace eKultura.EntityExtractor.Domain.PdfReading;

public class PdfTextReader : IPdfTextReader
{
    private readonly ILogger<PdfTextReader> _logger;

    public PdfTextReader(ILogger<PdfTextReader> logger)
    {
        _logger = logger;
    }

    public Task<TextDocument> ReadTextAsync(PdfDocument pdfDocument)
    {
        _logger.LogInformation("Attempting to open the memory stream for reading of {DocumentName}.", pdfDocument.Name);

        using var pdf = OpenDocument(pdfDocument.DocumentStream);

        _logger.LogInformation("Starting reading process of the memory stream.");

        var textDocumentPages = pdf.GetPages()
            .Select(p => new { p.Number, p.Text })
            .Join(ExtractPageLabels(pdfDocument.DocumentStream),
                doc => doc.Number,
                label => label.Key,
                (doc, label) => new TextDocumentPage(doc.Number, doc.Text, label.Value))
            .ToList();

        _logger.LogInformation("Successfully read {PageCount} pages of {DocumentName}.",
            pdf.NumberOfPages, pdfDocument.Name);

        return Task.FromResult(new TextDocument(pdfDocument.Name, textDocumentPages, pdfDocument.Topic));
    }

    public IEnumerable<KeyValuePair<int, string>> ExtractPageLabels(MemoryStream stream)
    {
        using var pdfMetadata = PdfReader.Open(stream, PdfDocumentOpenMode.Import);

        return pdfMetadata.GetPageLabels();
    }

    private static PdfPigDocument OpenDocument(MemoryStream stream)
    {
        try
        {
            return PdfPigDocument.Open(stream);
        }
        catch (PdfDocumentFormatException ex)
        {
            throw new InvalidOperationException("Provided file stream is not a PDF document.", ex);
        }
        catch (PdfDocumentEncryptedException ex)
        {
            throw new InvalidOperationException("Cannot read the PDF file because it is encrypted.", ex);
        }
    }
}