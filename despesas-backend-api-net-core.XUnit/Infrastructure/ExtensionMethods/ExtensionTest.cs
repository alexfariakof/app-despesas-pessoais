using despesas_backend_api_net_core.Infrastructure.ExtensionMethods;
using System.Globalization;

namespace Test.XUnit.Infrastructure.ExtensionMethods
{
    public class ExtensionTest
    {
        [Theory]
        [InlineData("123", 123)]
        [InlineData("42", 42)]
        [InlineData("0", 0)]
        public void ToInteger_Should_Convert_String_To_Integer(string input, int expected)
        {
            // Act
            int result = input.ToInteger();

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(123, 123)]
        [InlineData(42, 42)]
        [InlineData(0, 0)]
        public void ToInteger_Should_Convert_Object_To_Integer(object input, int expected)
        {
            // Act
            int result = input.ToInteger();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToDateBr_Should_Format_Date_As_DD_MM_YYYY()
        {
            // Arrange
            DateTime date = new DateTime(2023, 10, 20);

            // Act
            string formattedDate = date.ToDateBr();

            // Assert
            Assert.Equal("20/10/2023", formattedDate);
        }

        [Theory]
        [InlineData("20/10/2023", "20/10/2023")]
        [InlineData("2023-10-20", "2023-10-20")]
        public void ToDateTime_Should_Convert_String_To_DateTime(string input, string expected)
        {
            // Arrange
            CultureInfo cultureInfo = new CultureInfo("pt-BR");
            DateTime expectedDate = DateTime.Parse(expected, cultureInfo);

            // Act
            DateTime result = input.ToDateTime();

            // Assert
            Assert.Equal(expectedDate, result);
        }

        [Theory]
        [InlineData("123,45", 123.45)]
        [InlineData("42,0", 42.0)]
        [InlineData("0,5", 0.5)]
        public void ToDecimal_Should_Convert_String_To_Decimal(string input, decimal expected)
        {
            // Act
            decimal result = input.ToDecimal();

            // Assert
            Assert.Equal(expected, result);
        }

    }
}