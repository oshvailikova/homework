using Cysharp.Threading.Tasks;
using SaveSystem.Base;
using System.IO;
using UnityEngine;

namespace SaveSystem
{
    public class JsonFileStorage : IFileStorage
    {
        private readonly string filePath;

        public JsonFileStorage(string fileName = "SaveData.json")
        {
            filePath = Path.Combine(Application.persistentDataPath, fileName);
        }

        public void Write(string data)
        {
            try
            {
                using StreamWriter writer = new StreamWriter(filePath, false);
                writer.WriteLine(data);
            }
            catch (IOException ex)
            {
                Debug.LogError($"Failed to save data asynchronously to file: {ex.Message}");
            }
        }

        public string Read()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using StreamReader reader = new StreamReader(filePath);
                    return reader.ReadToEnd();
                }
            }
            catch (IOException ex)
            {
                Debug.LogError($"Failed to load data asynchronously from file: {ex.Message}");
            }
            return null;
        }

        public async UniTask WriteAsync(string data)
        {
            try
            {
                using StreamWriter writer = new StreamWriter(filePath, false);
                await writer.WriteLineAsync(data);
            }
            catch (IOException ex)
            {
                Debug.LogError($"Failed to save data asynchronously to file: {ex.Message}");
            }
        }

        public async UniTask<string> ReadAsync()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using StreamReader reader = new StreamReader(filePath);
                    return await reader.ReadToEndAsync();
                }
            }
            catch (IOException ex)
            {
                Debug.LogError($"Failed to load data asynchronously from file: {ex.Message}");
            }
            return null;
        }
    }
}
