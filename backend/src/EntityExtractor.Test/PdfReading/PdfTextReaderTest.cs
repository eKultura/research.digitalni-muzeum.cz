using eKultura.EntityExtractor.Domain.PdfReading;
using Microsoft.Extensions.Logging.Abstractions;
using System.Xml.Linq;
using UglyToad.PdfPig.Writer;

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

    [Fact]
    public async Task ReadEmptyPdfDocument_EmptyResultReturned()
    {
        // Arrange        
        using var memoryStream = new MemoryStream();

        using PdfDocumentBuilder builder = new PdfDocumentBuilder();
        byte[] pdfDoc = builder.Build();

        await memoryStream.WriteAsync(pdfDoc);
        memoryStream.Position = 0;

        // Act
        var result = await _pdfReader.ReadTextAsync(memoryStream);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(0, result.PageCount);
        Assert.Equal(0, result.WordCount);
        Assert.Empty(result.Text);
    }
}
