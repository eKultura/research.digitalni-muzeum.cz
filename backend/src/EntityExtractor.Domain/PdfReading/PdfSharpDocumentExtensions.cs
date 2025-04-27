using eKultura.EntityExtractor.Contracts;
using PdfSharpCore.Pdf;
using static PdfSharpCore.Pdf.PdfArray;
using PdfSharpDocument = PdfSharpCore.Pdf.PdfDocument;

namespace eKultura.EntityExtractor.Domain.PdfReading;

public static class PdfSharpDocumentExtensions
{
    /// <summary>
    /// Returns key-value pairs for physical and label page numbers.
    /// </summary>
    /// <param name="document">A PDF Sharp Document</param>
    /// <returns>A collection of physical page numbers and their labels</returns>
    public static IEnumerable<KeyValuePair<int, string>> GetPageLabels(this PdfSharpDocument document)
    {
        var pageLabelsDictionary = document.Internals.Catalog.Elements.GetDictionary(DocumentReadingConstants.PageLabelsElementName);
        var numberTree = pageLabelsDictionary?.Elements?.GetArray(DocumentReadingConstants.NumbersElementName);

        if (pageLabelsDictionary is null || numberTree is null)
        {
            return Enumerable.Empty<KeyValuePair<int, string>>();
        }

        var segments = GetNumericSegments(numberTree.Elements)
            .OrderBy(s => s.startIndex)
            .ToList();

        return ProcessNumericSegments(segments, document.PageCount);
    }

    private static IEnumerable<KeyValuePair<int, string>> ProcessNumericSegments(List<(int startIndex, PdfDictionary labelDict)> segments,
        int documentPages)
    {
        for (int segmentIndex = 0; segmentIndex < segments.Count; segmentIndex++)
        {
            var (startIndex, labelDict) = segments[segmentIndex];

            int endIndex = (segmentIndex < segments.Count - 1)
                ? segments[segmentIndex + 1].startIndex - 1
                : documentPages - 1;

            string prefix = labelDict.Elements.GetString("/P") ?? string.Empty;
            string type = labelDict.Elements.GetName("/S") ?? string.Empty;
            int startValue = labelDict.Elements.ContainsKey("/St") ? labelDict.Elements.GetInteger("/St") : 1;

            for (int pageIndex = startIndex; pageIndex <= endIndex; pageIndex++)
            {
                int pageNumber = pageIndex + 1;
                int labelNumber = startValue + (pageIndex - startIndex);

                string formattedNumber = FormatPageNumber(labelNumber, type);
                string pageLabel = prefix + formattedNumber;

                yield return KeyValuePair.Create(pageNumber, pageLabel);
            }
        }
    }

    private static IEnumerable<(int startIndex, PdfDictionary labelDict)> GetNumericSegments(ArrayElements elements)
    {
        for (int i = 0; i < elements.Count; i += 2)
        {
            if (i + 1 < elements.Count)
            {
                int startIndex = elements.GetInteger(i);
                var labelDict = elements.GetDictionary(i + 1);

                if (labelDict != null)
                {
                    yield return (startIndex, labelDict);
                }
            }
        }
    }

    private static string FormatPageNumber(int number, string style)
    {
        return style switch
        {
            "/D" => number.ToString(),
            "/R" => NumberUtils.ToRoman(number).ToUpper(),
            "/r" => NumberUtils.ToRoman(number).ToLower(),
            "/A" => NumberUtils.ToLetters(number).ToUpper(),
            "/a" => NumberUtils.ToLetters(number).ToLower(),
            _ => string.Empty
        };
    }
}