namespace Domain.Core.Interface;
public interface ICrypto
{
    string Encrypt(string password);
    string Decrypt(string encryptedText);
}