using Microsoft.Extensions.Logging;
using System.Text;
using UglyToad.PdfPig;

namespace eKultura.EntityExtractor.Domain.PdfReading;

public class PdfTextReader
{
    private const char SpaceDelimiter = ' ';

    private readonly ILogger<PdfTextReader> _logger;

    public PdfTextReader(ILogger<PdfTextReader> logger)
    {
        _logger = logger;
    }

    public Task<string> ReadAsync(MemoryStream stream)
    {
        _logger.LogInformation("Attempting to open the memory stream for reading.");

        using var pdf = PdfDocument.Open(stream);      

        _logger.LogInformation("Starting reading process of the memory stream.");

        StringBuilder stringBuilder = new StringBuilder();
        int pageCount = 1;

        foreach (var page in pdf.GetPages())
        {
            var wordsOnPage = page.GetWords().Select(w => w.Text);
            string pageString = string.Join(SpaceDelimiter, wordsOnPage);

            stringBuilder.Append(pageString);
            pageCount++;
        }

        _logger.LogInformation("Successfully read {PageCount} pages of the pdf document.", pageCount);

        return Task.FromResult(stringBuilder.ToString());
    }
}
