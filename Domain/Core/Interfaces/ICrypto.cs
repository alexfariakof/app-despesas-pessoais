namespace Domain.Core.Interfaces;
public interface ICrypto
{
    string Encrypt(string password);
    string Decrypt(string encryptedText);
}