using eKultura.EntityExtractor.Contracts;
using Microsoft.Extensions.Logging;
using PdfSharpCore.Pdf.IO;
using System.Text;
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
        _logger.LogInformation("Attempting to open the memory stream for reading.");

        using var pdf = OpenDocument(pdfDocument.DocumentStream);

        _logger.LogInformation("Starting reading process of the memory stream.");

        StringBuilder stringBuilder = new StringBuilder();
        int pageCount = 0;
        int wordCount = 0;

        foreach (var page in pdf.GetPages())
        {
            pageCount++;

            var wordsOnPage = page.GetWords().Select(w => w.Text);
            wordCount += wordsOnPage.Count();

            string pageString = string.Join(DocumentReadingConstants.SpaceDelimiter, wordsOnPage);

            stringBuilder.Append(pageString + DocumentReadingConstants.SpaceDelimiter);
        }

        _logger.LogInformation("Successfully read {WordCount} words on {PageCount} pages of the pdf document.",
            wordCount, pageCount);

        return Task.FromResult(new TextDocument(pageCount, wordCount, stringBuilder.ToString().Trim()));
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