using eKultura.EntityExtractor.Contracts;
using eKultura.EntityExtractor.Domain.DocumentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKultura.EntityExtractor.Test.DocumentValidation;

public class PdfValidatorTest
{
    private readonly PdfValidator _pdfValidator = new();

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


}
