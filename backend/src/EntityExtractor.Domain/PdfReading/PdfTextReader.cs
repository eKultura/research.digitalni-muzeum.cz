using Microsoft.Extensions.Logging;
using System.Text;
using PdfPigDocument = UglyToad.PdfPig.PdfDocument;

namespace eKultura.EntityExtractor.Domain.PdfReading;

public class PdfTextReader
{
    private const char SpaceDelimiter = ' ';

    private readonly ILogger<PdfTextReader> _logger;

    public PdfTextReader(ILogger<PdfTextReader> logger)
    {
        _logger = logger;
    }

    public Task<PdfDocument> ReadTextAsync(MemoryStream stream)
    {
        _logger.LogInformation("Attempting to open the memory stream for reading.");

        using var pdf = PdfPigDocument.Open(stream);      

        _logger.LogInformation("Starting reading process of the memory stream.");

        StringBuilder stringBuilder = new StringBuilder();
        int pageCount = 0;
        int wordCount = 0;

        foreach (var page in pdf.GetPages())
        {
            pageCount++;

            var wordsOnPage = page.GetWords().Select(w => w.Text);
            wordCount += wordsOnPage.Count();

            string pageString = string.Join(SpaceDelimiter, wordsOnPage);

            stringBuilder.Append(pageString);            
        }

        _logger.LogInformation("Successfully read {WordCount} words on {PageCount} pages of the pdf document.", 
            wordCount, pageCount);

        return Task.FromResult(new PdfDocument(pageCount, wordCount, stringBuilder.ToString()));
    }
}
