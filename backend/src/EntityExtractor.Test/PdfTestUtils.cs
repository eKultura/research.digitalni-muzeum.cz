using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Fonts.Standard14Fonts;
using UglyToad.PdfPig.Writer;

namespace eKultura.EntityExtractor.Test;

internal static class PdfTestUtils
{
    internal static byte[] BuildPdf(string[] pages)
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
}
