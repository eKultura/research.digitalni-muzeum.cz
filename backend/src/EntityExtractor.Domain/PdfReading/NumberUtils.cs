using System.Text;

namespace eKultura.EntityExtractor.Domain.PdfReading;

public static class NumberUtils
{
    private const int EnglishAlphabetCharacterCount = 26;

    private static readonly IReadOnlyDictionary<int, string> RomanNumerals = new Dictionary<int, string>
    {
        { 1000, "M" },
        { 900, "CM" },
        { 500, "D" },
        { 400, "CD" },
        { 100, "C" },
        { 90, "XC" },
        { 50, "L" },
        { 40, "XL" },
        { 10, "X" },
        { 9, "IX" },
        { 5, "V" },
        { 4, "IV" },
        { 1, "I" }
    };

    /// <summary>
    /// Converts a string to roman number
    /// </summary>
    /// <param name="number">Integer to convert</param>
    /// <returns>A string representing integer in a roman form. Empty string if the number is not a positive integer.</returns>
    public static string ToRoman(int number)
    {
        if (number <= 0)
        {
            return string.Empty;
        }

        var result = new StringBuilder();

        foreach (var numeral in RomanNumerals)
        {
            while (number >= numeral.Key)
            {
                result.Append(numeral.Value);
                number -= numeral.Key;
            }
        }

        return result.ToString();
    }

    /// <summary>
    /// Implementation of number to letters conversion to an English alphabet character
    /// </summary>
    /// <param name="number">An integer to convert</param>
    /// <returns>An integer represented in a letter from the English alphabet</returns>
    public static string ToLetters(int number)
    {
        if (number <= 0)
        {
            return string.Empty;
        }

        var result = new StringBuilder();

        while (number > 0)
        {
            number--;
            result.Insert(0, (char)('A' + number % EnglishAlphabetCharacterCount));
            number /= EnglishAlphabetCharacterCount;
        }

        return result.ToString();
    }
}
