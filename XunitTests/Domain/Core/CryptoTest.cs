using Cryptography;

namespace Domain.Core.Cryptography;
public sealed class CryptoTest
{
    public CryptoTest() { }

    [Fact]
    public void Encrypt_And_Decrypt_Should_Work()
    {
        // Arrange
        string originalText = "!12345";
        ICrypto crypto = Crypto.Instance;

        // Act
        string encryptedText = crypto.Encrypt(originalText);
        var IsEquals = crypto.IsEquals(originalText, encryptedText);


        // Assert
        Assert.NotEqual(originalText, encryptedText);
        Assert.True(IsEquals);
    }

    [Fact]
    public void Encrypt_Should_Produce_Different_Output_For_Same_Input()
    {
        // Arrange
        string originalText = "!12345";
        ICrypto crypto = Crypto.Instance;

        // Act
        string encryptedText1 = crypto.Encrypt(originalText);
        string encryptedText2 = crypto.Encrypt(originalText);

        // Assert
        Assert.NotEqual(encryptedText1, encryptedText2);
    }
}
