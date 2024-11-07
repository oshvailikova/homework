using Cysharp.Threading.Tasks;
using SaveSystem.Base;
using SaveSystem.Utils.Base;
using Zenject;

namespace SaveSystem
{
    public class FileSaveLoadSystem : ISaveLoadSystem
    {
        private readonly IEncryptor _encryptor;
        private readonly IFileStorage _fileStorage;

        [Inject]
        public FileSaveLoadSystem(IEncryptor encryptor, IFileStorage fileStorage)
        {
            _encryptor = encryptor;
            _fileStorage = fileStorage;
        }

        public void Save(string data)
        {
            var encryptedData = _encryptor.Encrypt(data);
            _fileStorage.Write(encryptedData);
        }

        public string Load()
        {
            var encryptedData = _fileStorage.Read();
            return _encryptor.Decrypt(encryptedData);
        }

        public async UniTask SaveAsync(string data)
        {
            var encryptedData = await _encryptor.EncryptAsync(data);
            await _fileStorage.WriteAsync(encryptedData);
        }

        public async UniTask<string> LoadAsync()
        {
            var encryptedData = await _fileStorage.ReadAsync();
            return await _encryptor.DecryptAsync(encryptedData);
        }
    }
}