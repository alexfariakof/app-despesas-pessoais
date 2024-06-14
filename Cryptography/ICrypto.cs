namespace Cryptography;
public interface ICrypto
{
    string Encrypt(string password);
    bool IsEquals(string plaintText, string encryptedText);
}