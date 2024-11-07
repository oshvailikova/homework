using Cysharp.Threading.Tasks;
using SaveSystem.Utils.Base;
using System;
using System.IO;
using System.Security.Cryptography;

namespace SaveSystem.Utils
{
    public class AesEncryptor : IEncryptor
    {
        private readonly string _key = "Lia9vJDb527nHbuNRA0OSQ==";
        private readonly string _iv = "DxToXX17V1WOoaEeytAp6Q==";

        private byte[] GetKeyBytes() => Convert.FromBase64String(_key);
        private byte[] GetIVBytes() => Convert.FromBase64String(_iv);

        public string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = GetKeyBytes();
            aes.IV = GetIVBytes();

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public string Decrypt(string cipherText)
        {
            using var aes = Aes.Create();
            aes.Key = GetKeyBytes();
            aes.IV = GetIVBytes();

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText));
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);

            return srDecrypt.ReadToEnd();
        }

        public async UniTask<string> EncryptAsync(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = GetKeyBytes();
            aes.IV = GetIVBytes();
            
            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                await swEncrypt.WriteAsync(plainText);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public async UniTask<string> DecryptAsync(string cipherText)
        {
            using var aes = Aes.Create();
            aes.Key = GetKeyBytes();
            aes.IV = GetIVBytes();

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText));
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);

            return await srDecrypt.ReadToEndAsync();
        }
    }
}
