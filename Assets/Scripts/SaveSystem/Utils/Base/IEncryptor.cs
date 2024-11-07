using Cysharp.Threading.Tasks;

namespace SaveSystem.Utils.Base
{
    public interface IEncryptor
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);

        UniTask<string> EncryptAsync(string plainText);
        UniTask<string> DecryptAsync(string cipherText);
    }
}