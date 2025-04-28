using eKultura.EntityExtractor.Contracts;
using Microsoft.Extensions.Logging;
using PdfSharpCore.Pdf.IO;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Exceptions;
using PdfPigDocument = UglyToad.PdfPig.PdfDocument;

namespace eKultura.EntityExtractor.Domain.PdfReading;

public class PdfTextReader : IPdfTextReader
{
    private const double ProportionOfPageBelongingToHeader = 0.1;

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

        var pages = pdf.GetPages()
            .Select(p => new 
            {
                PageNumber = p.Number,
                Text = string.Join(DocumentReadingConstants.SpaceDelimiter,
                    p.GetWords()
                    .Where(w => !BelongsToHeader(p, w))
                    .Select(w => w.Text))
            })
            .Join(ExtractPageLabels(pdfDocument.DocumentStream),
                p => p.PageNumber,
                l => l.Key,
                (p, l) => new TextDocumentPage(p.PageNumber, p.Text, l.Value))
            .ToList();

        _logger.LogInformation("Successfully read {PageCount} pages of {DocumentName}.",
            pdf.NumberOfPages, pdfDocument.Name);

        return Task.FromResult(new TextDocument(pdfDocument.Name, pages, pdfDocument.Topic));
    }

    private IEnumerable<KeyValuePair<int, string>> ExtractPageLabels(MemoryStream stream)
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

    private static bool BelongsToHeader(Page page, Word word)
    {
        return word.BoundingBox.Bottom >= (page.Height - (page.Height * ProportionOfPageBelongingToHeader));
    }
}