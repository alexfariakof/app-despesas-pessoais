using Microsoft.Extensions.Options;
using __mock__;
using Microsoft.Extensions.Configuration;

namespace Cryptography;

public sealed class CryptoTest
{
    private readonly IOptions<CryptoOptions?> _cryptoOptions;

    public CryptoTest()
    {
        var configuration = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                       .Build();

        if (configuration is null) throw new ArgumentNullException("Arquivo appsettings.json não encontrado.");
        _cryptoOptions = Options.Create(configuration.GetSection("Crypto").Get<CryptoOptions>()) ?? throw new ArgumentNullException("Configurações appsettings.json não encontradas.");
    }

    [Fact]
    public void Encrypt_And_Decrypt_Should_Work_With_Instance()
    {
        // Arrange
        string originalText = MockCrypto.Instance.GetNewPlainText();
        ICrypto crypto = Crypto.Instance;

        // Act
        string encryptedText = crypto.Encrypt(originalText);
        var isEquals = crypto.IsEquals(originalText, encryptedText);

        // Assert
        Assert.NotEqual(originalText, encryptedText);
        Assert.True(isEquals);
    }

    [Fact]
    public void Encrypt_And_Decrypt_Should_Work_With_Options_Instance()
    {
        // Arrange
        string originalText = MockCrypto.Instance.GetNewPlainText();
        ICrypto crypto = new Crypto(_cryptoOptions);

        // Act
        string encryptedText = crypto.Encrypt(originalText);
        var isEquals = crypto.IsEquals(originalText, encryptedText);

        // Assert
        Assert.NotEqual(originalText, encryptedText);
        Assert.True(isEquals);
    }

    [Fact]
    public void Encrypt_Should_Produce_Different_Output_For_Same_Input()
    {
        // Arrange
        string originalText = MockCrypto.Instance.GetNewPlainText();
        ICrypto crypto = Crypto.Instance;

        // Act
        string encryptedText1 = crypto.Encrypt(originalText);
        string encryptedText2 = crypto.Encrypt(originalText);

        // Assert
        Assert.NotEqual(encryptedText1, encryptedText2);
    }

    [Fact]
    public void Encrypt_Should_Produce_Different_Output_For_Same_Input_With_Options_Instance()
    {
        // Arrange
        string originalText = MockCrypto.Instance.GetNewPlainText();
        ICrypto crypto = new Crypto(_cryptoOptions);

        // Act
        string encryptedText1 = crypto.Encrypt(originalText);
        string encryptedText2 = crypto.Encrypt(originalText);

        // Assert
        Assert.NotEqual(encryptedText1, encryptedText2);
    }

    [Fact]
    public void Encrypt_Should_Produce_Valid_Hash_With_Salt()
    {
        // Arrange
        string originalText = MockCrypto.Instance.GetNewPlainText();
        ICrypto crypto = Crypto.Instance;

        // Act
        string encryptedText = crypto.Encrypt(originalText);
        var isEquals = crypto.IsEquals(originalText, encryptedText);

        // Assert
        Assert.True(isEquals);
    }

    [Fact]
    public void Encrypt_Should_Produce_Valid_Hash_With_Salt_With_Options_Instance()
    {
        // Arrange
        string originalText = MockCrypto.Instance.GetNewPlainText();
        ICrypto crypto = new Crypto(_cryptoOptions);

        // Act
        string encryptedText = crypto.Encrypt(originalText);
        var isEquals = crypto.IsEquals(originalText, encryptedText);

        // Assert
        Assert.True(isEquals);
    }

    [Fact]
    public void Encrypt_Should_Produce_Valid_Hash_With_Salt_With_Instance_and_Options_Instance()
    {
        // Arrange
        string originalText = MockCrypto.Instance.GetNewPlainText();
        ICrypto crypto = Crypto.Instance;
        ICrypto cryptoOptions = new Crypto(_cryptoOptions);

        // Act
        string encryptedTextInstance = crypto.Encrypt(originalText);

        var isEquals = crypto.IsEquals(originalText, encryptedTextInstance);
        var isEqualsOptions = cryptoOptions.IsEquals(originalText, encryptedTextInstance);

        // Assert
        Assert.True(isEquals);
        Assert.True(isEqualsOptions);
    }

    [Fact]
    public void Encrypt_And_IsEquals_Should_Handle_Empty_Input()
    {
        // Arrange
        string originalText = "";
        ICrypto crypto = Crypto.Instance;

        // Act
        string encryptedText = crypto.Encrypt(originalText);
        var isEquals = crypto.IsEquals(originalText, encryptedText);

        // Assert
        Assert.NotEqual(originalText, encryptedText);
        Assert.True(isEquals);
    }

    [Fact]
    public void Encrypt_And_IsEquals_Should_Handle_Empty_Input_With_Options_Instance()
    {
        // Arrange
        string originalText = "";
        ICrypto crypto = new Crypto(_cryptoOptions);

        // Act
        string encryptedText = crypto.Encrypt(originalText);
        var isEquals = crypto.IsEquals(originalText, encryptedText);

        // Assert
        Assert.NotEqual(originalText, encryptedText);
        Assert.True(isEquals);
    }

    [Theory]
    [InlineData("FF23456789EBCDEF0123456789ABCDEA", true)]  // Chave válida
    [InlineData("9g23456789EBCDEF0123456789ABCDEA", false)] // Chave inválida (contém caracteres não hexadecimais)
    [InlineData("FF23456789EBCDEF", false)]     // Chave inválida (tamanho diferente de 32)
    public void ValidateKey_Should_Throw_Exception_For_Invalid_Keys(string key, bool isValid)
    {
        // Arrange
        var options = Options.Create(new CryptoOptions()
        {
            Key = key,
            AuthSalt = "SRTGE}46aSb$"
        });

        // Act & Assert
        if (isValid)
        {
            Assert.IsType<Crypto>(new Crypto(options));
        }
        else
        {
            Assert.Throws<ArgumentException>(() => new Crypto(options));
        }
    }

    [Theory]
    [InlineData("FF23456789EBCDEF0123456789ABCDEA", true)] // Chave válida
    [InlineData("09ABCDEF23456789abcdef0123456789", true)] // Chave válida
    [InlineData("0123456789ABCDEFabcdef0123456789", true)] // Chave válida
    [InlineData("FF23456789EBCDEF0123456789ABCDEF", true)] // Chave válida
    [InlineData("0123456789abcdefABCDEF0123456789", true)] // Chave válida
    [InlineData("9g23456789EBCDEF0123456789ABCDEA", false)] // Chave inválida
    [InlineData("09:BCDEF23456789abcdef0123456789", false)] // Chave inválida
    [InlineData("0123456789ABCGDEFabcdef012345678", false)] // Chave inválida
    [InlineData("FF23456789EBZDEF0123456789ABCDEF", false)] // Chave inválida
    [InlineData("0123456789abc:efABCDEF0123456789", false)] // Chave inválida
    public void IsHexadecimalDigit_Should_Return_Correct_Result(string key, bool isValid)
    {
        // Arrange
        var options = Options.Create(new CryptoOptions()
        {
            Key = key,
            AuthSalt = "SRTGE}46aSb$"
        });

        // Act & Assert
        if (isValid)
        {
            Assert.IsType<Crypto>(new Crypto(options));
        }
        else
        {
            Assert.Throws<ArgumentException>(() => new Crypto(options));
        }
    }
}