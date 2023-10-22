﻿using despesas_backend_api_net_core.Infrastructure.Security.Implementation;
using System.Security.Cryptography;

namespace Test.XUnit.Infrastructure.Security.Implementation
{
    public class CryptoTest
    {
        public CryptoTest() { }

        [Fact]
        public void Encrypt_And_Decrypt_Should_Work()
        {
            // Arrange
            string key = "01010101010101010101010101010101";
            string originalText = "!12345";
            Crypto crypto = Crypto.GetInstance;

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
            string key = "01010101010101010101010101010101";
            string originalText = "!12345";
            Crypto crypto = Crypto.GetInstance;

            // Act
            string encryptedText1 = crypto.Encrypt(originalText);
            string encryptedText2 = crypto.Encrypt(originalText);

            // Assert
            Assert.NotEqual(encryptedText1, encryptedText2);
        }
    }
}
