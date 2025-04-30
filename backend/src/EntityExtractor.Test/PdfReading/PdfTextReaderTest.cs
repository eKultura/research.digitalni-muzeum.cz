using eKultura.EntityExtractor.Contracts;
using eKultura.EntityExtractor.Domain.PdfReading;
using Microsoft.Extensions.Logging.Abstractions;
using PdfSharpCore.Pdf.IO;
using System.Xml.Linq;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Fonts.Standard14Fonts;
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

        var pdfDocument = new PdfDocument("Root Doc", memoryStream.ToArray(), "Sci-fi");

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _pdfReader.ReadTextAsync(pdfDocument));
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

        var pdfDocument = new PdfDocument("Empty Doc", memoryStream.ToArray(), "Empty");

        // Act
        var result = await _pdfReader.ReadTextAsync(pdfDocument);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(pdfDocument.Name, result.Name);
        Assert.Equal(pdfDocument.Topic, result.Topic);
        Assert.Empty(result.Pages);
    }

    [Theory]
    [MemberData(nameof(GetDocuments))]
    public async Task ReadPdfDocument_CorrectResultReturned(string[] pages)
    {
        // Arrange        
        using var memoryStream = new MemoryStream();

        var words = pages.Select(p => p.Split(DocumentReadingConstants.SpaceDelimiter)).SelectMany(p => p);
        int wordCount = words.Count();
        string text = string.Join(DocumentReadingConstants.SpaceDelimiter, words);

        byte[] pdfDoc = BuildPdf(pages);

        await memoryStream.WriteAsync(pdfDoc);
        memoryStream.Position = 0;

        var pdfDocument = new PdfDocument("Important doc.pdf", memoryStream.ToArray(), "Fiction");

        // Act
        var result = await _pdfReader.ReadTextAsync(pdfDocument);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(pdfDocument.Name, result.Name);
        Assert.Equal(pdfDocument.Topic, result.Topic);

        Assert.Equal(pages.Length, result.Pages.Count);

        var resultPages = result.Pages;

        for (int i = 0; i < resultPages.Count; i++)
        {
            var textPage = resultPages[i];

            //PDF pages start from 1
            Assert.Equal(i + 1, textPage.Number);
            Assert.Equal(pages[i], result.Pages[i].Text);
        }
    }

    [Fact]
    public async Task Bastard()
    {
        string path = "e:\\Osobne\\Knižky\\Hans-Hermann Hoppe - Democracy_ The God that Failed.pdf";
        //string path = "e:\\Osobne\\Knižky\\The clean coder  a code of conduct for professional programmers by Martin, Robert C..pdf";

        using var memoryStream = new MemoryStream();
        memoryStream.Write(File.ReadAllBytes(path));
        memoryStream.Position = 0;

        var pdfDoc = new PdfDocument("Bastard", memoryStream.ToArray(), "Fiction");

        var result = await _pdfReader.ReadTextAsync(pdfDoc);
    }

    private static byte[] BuildPdf(string[] pages)
    {
        using PdfDocumentBuilder builder = new PdfDocumentBuilder();

        foreach (string page in pages)
        {
            var pdfPageBuilder = builder.AddPage(PageSize.A4);

            var font = builder.AddStandard14Font(Standard14Font.TimesRoman);

            pdfPageBuilder.AddText(page, 12, new PdfPoint(25, 700), font);
        }

        return builder.Build();
    }

    public static IEnumerable<object[]> GetDocuments()
    {
        yield return new object[]
        {
            new string[] { "This page intentionally left blank" }
        };

        yield return new object[]
        {
            new string[]
            {
                "This page intentionally left blank",
                "But it wasn't really blank, was it?"
            },
        };

        yield return new object[]
        {
            new string[]
            {
                "The Count of Monte Cristo",
                "Alexandre Dumas",
                "The Count of Monte Cristo is an adventure novel by French author Alexandre Dumas serialized from 1844 to 1846, " +
                "and published in book form in 1846. It is one of the author's most popular works, " +
                "along with The Three Musketeers and Man in the Iron Mask."
            }
        };
    }
}