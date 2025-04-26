using eKultura.EntityExtractor.Contracts;
using Microsoft.Extensions.Logging;
using PdfSharpCore.Pdf.IO;
using System.Text;
using UglyToad.PdfPig.Content;
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
            .Select(p => new TextDocumentPage(p.Number, p.Text))
            .ToList();

        _logger.LogInformation("Successfully read {PageCount} pages of {DocumentName}.",
            pdf.NumberOfPages, pdfDocument.Name);

        return Task.FromResult(new TextDocument(pdfDocument.Name, textDocumentPages, pdfDocument.Topic));
    }

    public IEnumerable<string> ExtractPageLabels(MemoryStream stream)
    {
        using var pdfMetadata = PdfReader.Open(stream, PdfDocumentOpenMode.Import);

        var labels = pdfMetadata.Internals.Catalog.Elements.GetDictionary(DocumentReadingConstants.PageLabelsElementName);

        if (labels is null)
        {
            yield break;
        }

        var nums = labels.Elements.GetArray(DocumentReadingConstants.NumbersElementName);

        if (nums is null)
        {
            yield break;
        }



        //foreach (var keke in pdfMetadata.Internals.Catalog.Elements)
        //{
        //    keke.Value.
        //}
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