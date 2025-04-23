using Microsoft.Extensions.Logging;
using System.Text;
using UglyToad.PdfPig;

namespace eKultura.EntityExtractor.Domain.PdfReading;

public class PdfTextReader
{
    private readonly ILogger<PdfTextReader> _logger;

    public PdfTextReader(ILogger<PdfTextReader> logger)
    {
        _logger = logger;
    }

    public Task<string> ReadAsync(MemoryStream stream)
    {
        _logger.LogInformation("Attempting to open the memory stream for reading.");

        using var pdf = PdfDocument.Open(stream);
        StringBuilder stringBuilder = new StringBuilder();

        _logger.LogInformation("Starting reading process of the memory stream.");

        foreach (var page in pdf.GetPages())
        {
            stringBuilder.Append(page.GetWords());
        }

        return Task.FromResult(stringBuilder.ToString());
    }
}
