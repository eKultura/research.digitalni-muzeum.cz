using eKultura.EntityExtractor.Domain.PdfReading;

namespace eKultura.EntityExtractor.Test.PdfReading;

public class NumberUtilsTest
{
    [Theory]
    [InlineData(0, "")]
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
}
