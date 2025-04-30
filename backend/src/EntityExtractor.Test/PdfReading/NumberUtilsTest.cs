using eKultura.EntityExtractor.Domain.PdfReading;

namespace eKultura.EntityExtractor.Test.PdfReading;

public class NumberUtilsTest
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-18)]
    public void ConvertInvalidNumber_ExceptionThrown(int number)
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => NumberUtils.ToRoman(number));
    }

    [Theory]
    [InlineData(1, "I")]
    [InlineData(2, "II")]
    [InlineData(3, "III")]
    [InlineData(4, "IV")]
    [InlineData(5, "V")]
    [InlineData(6, "VI")]
    [InlineData(7, "VII")]
    [InlineData(8, "VIII")]
    [InlineData(9, "IX")]
    public void ConvertOnes_CorrectValueReturned(int number, string expectedResult)
    {
        // Act
        string result = NumberUtils.ToRoman(number);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(10, "X")]
    [InlineData(14, "XIV")]
    [InlineData(19, "XIX")]
    [InlineData(20, "XX")]
    [InlineData(40, "XL")]
    [InlineData(50, "L")]
    [InlineData(66, "LXVI")]
    [InlineData(90, "XC")]
    public void ConvertTens_CorrectValueReturned(int number, string expectedResult)
    {
        // Act
        string result = NumberUtils.ToRoman(number);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(101, "CI")]
    [InlineData(150, "CL")]
    [InlineData(199, "CXCIX")]
    [InlineData(256, "CCLVI")]
    [InlineData(378, "CCCLXXVIII")]
    [InlineData(499, "CDXCIX")]
    [InlineData(501, "DI")]
    [InlineData(555, "DLV")]
    [InlineData(678, "DCLXXVIII")]
    [InlineData(999, "CMXCIX")]
    public void Convert_Hundreds_ReturnsCorrectRomanNumeral(int input, string expectedResult)
    {
        // Act
        string result = NumberUtils.ToRoman(input);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(58, "LVIII")]
    [InlineData(3749, "MMMDCCXLIX")]
    [InlineData(1994, "MCMXCIV")]
    [InlineData(3549, "MMMDXLIX")]
    [InlineData(3999, "MMMCMXCIX")]
    public void Convert_ComplexNumbers_ReturnsCorrectRomanNumeral(int input, string expected)
    {
        // Act
        string result = NumberUtils.ToRoman(input);

        // Assert
        Assert.Equal(expected, result);
    }
}