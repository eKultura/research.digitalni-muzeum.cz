using System.Text;

namespace eKultura.EntityExtractor.Domain.PdfReading;

public static class NumberUtils
{
    private const int EnglishAlphabetCharacterCount = 26;

    private static readonly string[] RomanThousands = new[] { "", "M", "MM", "MMM", "MMMM" };
    private static readonly string[] RomanHundreds = new[] { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
    private static readonly string[] RomanTens = new[] { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
    private static readonly string[] RomanOnes = new[] { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };

    /// <summary>
    /// Converts a string to roman number
    /// </summary>
    /// <param name="number">Integer to convert</param>
    /// <returns>A string representing integer in a roman form. Empty string if the number is not a positive integer.</returns>
    public static string ToRoman(int number)
    {
        if (number <= 0)
        {
            throw new ArgumentOutOfRangeException($"{number} cannot be converted to roman number.");
        }

        return RomanThousands[number / 1000] + RomanHundreds[(number % 1000) / 100]
            + RomanTens[(number % 100) / 10] + RomanOnes[number % 10];
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