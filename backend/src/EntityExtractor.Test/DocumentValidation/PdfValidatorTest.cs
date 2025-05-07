using eKultura.EntityExtractor.Contracts;
using eKultura.EntityExtractor.Domain.DocumentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKultura.EntityExtractor.Test.DocumentValidation;

public class PdfValidatorTest
{
    private readonly PdfValidator _pdfValidator = new(NullLogger<PdfValidator>.Instance);

    [Fact]
    public void IsValid_WrongExtension_ReturnsFalse()
    {
        // Arrange
        var pdf = PdfTestUtils.BuildPdf(["Test pdf"]);

        var pdfDocument = new PdfDocument("test.docx", pdf, "History");

        // Act
        bool isValid = _pdfValidator.IsValid(pdfDocument);

        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public void IsValid_NoPdfSignature_ReturnsFalse()
    {
        // Arrange
        string text = "This is absolutely not a PDF document";
        byte[] bytes = Encoding.UTF8.GetBytes(text);

        var document = new PdfDocument("test.pdf", bytes, "Computer science");

        // Act
        bool isValid = _pdfValidator.IsValid(document);

        // Assert
        Assert.False(isValid);

    }
}
