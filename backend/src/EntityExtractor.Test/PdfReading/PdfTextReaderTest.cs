using eKultura.EntityExtractor.Domain.PdfReading;
using Microsoft.Extensions.Logging.Abstractions;
using System.Xml.Linq;

namespace eKultura.EntityExtractor.Test.PdfReading;

public class PdfTextReaderTest
{
    private readonly PdfTextReader _pdfReader = new(NullLogger<PdfTextReader>.Instance);

    [Fact]
    public async Task ReadNonPdfDocument_InvalidOperationExceptionThrown()
    {
        // Arrange        
        using var memoryStream = new MemoryStream();

        XDocument xmlDoc = new XDocument();

        var root = new XElement(XName.Get("RootDocument"), "This is root");
        xmlDoc.Add(root);

        xmlDoc.Save(memoryStream);

        memoryStream.Position = 0;

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _pdfReader.ReadTextAsync(memoryStream));
    }

}
