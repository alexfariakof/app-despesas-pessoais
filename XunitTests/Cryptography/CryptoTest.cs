﻿using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using __mock__;
using EasyCryptoSalt;

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
        _cryptoOptions = Options.Create(configuration.GetSection("CryptoConfigurations").Get<CryptoOptions>()) ?? throw new ArgumentNullException("Configurações appsettings.json não encontradas.");
    }

    [Fact]
    public void Encrypt_And_Decrypt_Should_Work_With_Instance()
    {
        // Arrange
        string originalText = MockCrypto.Instance.GetNewPlainText();
        ICrypto crypto = Crypto.Instance;

        // Act
        string encryptedText = crypto.Encrypt(originalText);
        var isEquals = crypto.Verify(originalText, encryptedText);

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
        var isEquals = crypto.Verify(originalText, encryptedText);

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
        var isEquals = crypto.Verify(originalText, encryptedText);

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
        var isEquals = crypto.Verify(originalText, encryptedText);

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

        var isEquals = crypto.Verify(originalText, encryptedTextInstance);
        var isEqualsOptions = cryptoOptions.Verify(originalText, encryptedTextInstance);

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
        var isEquals = crypto.Verify(originalText, encryptedText);

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
        var isEquals = crypto.Verify(originalText, encryptedText);

        // Assert
        Assert.NotEqual(originalText, encryptedText);
        Assert.True(isEquals);
    }
}