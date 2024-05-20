using Domain.Core.Interfaces;

namespace Domain.Core;
public sealed class CryptoTest
{
    public CryptoTest() { }

    [Fact]
    public void Encrypt_And_Decrypt_Should_Work()
    {
        // Arrange
        string originalText = "!12345";
        ICrypto crypto = Crypto.GetInstance;

        // Act
        string encryptedText = crypto.Encrypt(originalText);
        string decryptedText = crypto.Decrypt(encryptedText);

        // Assert
        Assert.NotEqual(originalText, encryptedText);
        Assert.Equal(originalText, decryptedText);
    }

    [Fact]
    public void Encrypt_Should_Produce_Different_Output_For_Same_Input()
    {
        // Arrange
        string originalText = "!12345";
        ICrypto crypto = Crypto.GetInstance;

        // Act
        string encryptedText1 = crypto.Encrypt(originalText);
        string encryptedText2 = crypto.Encrypt(originalText);

        // Assert
        Assert.NotEqual(encryptedText1, encryptedText2);
    }
}
