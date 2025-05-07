using eKultura.EntityExtractor.Contracts;
using Microsoft.Extensions.Logging;

namespace eKultura.EntityExtractor.Domain.DocumentValidation;

public class PdfValidator
{
    private const string PdfExtension = ".pdf";

    private readonly ILogger<PdfValidator> _logger;

    public PdfValidator(ILogger<PdfValidator> logger)
    {
        _logger = logger;
    }

    public bool IsValid(PdfDocument pdfDocument)
    {
        return IsDocumentPdf(pdfDocument);
    }

    private bool IsDocumentPdf(PdfDocument document)
    {
        bool hasPdfExtension = Path.GetExtension(document.Name).Equals(PdfExtension, StringComparison.OrdinalIgnoreCase);

        if (!hasPdfExtension)
        {
            _logger.LogWarning("The uploaded file '{FileName}' does not have a pdf extension.", document.Name);
            return false;
        }

        if (!HasPdfSignature(document.Document))
        {
            _logger.LogWarning("The uploaded file '{FileName}' is not a PDF document.", document.Name);
            return false;
        }

        return true;
    }



    /// <summary>
    /// Determines whether the file has the PDF signature.
    /// </summary>
    /// <param name="bytes">A file in binary format</param>
    /// <returns>True if the file has the PDF signature. Otherwise false</returns>
    /// <remarks>The PDF files have a file signature that starts with "%PDF-" (25 50 44 46 2D in hex)</remarks>
    private bool HasPdfSignature(byte[] bytes)
    {
        return bytes.Length >= 5 &&
               bytes[0] == 0x25 && // %
               bytes[1] == 0x50 && // P
               bytes[2] == 0x44 && // D
               bytes[3] == 0x46 && // F
               bytes[4] == 0x2D; // -
    }
}
