using Cysharp.Threading.Tasks;

namespace SaveSystem.Base
{
    public interface ISaveLoadSystem
    {
        void Save(string data);
        string Load();

        UniTask SaveAsync(string data);
        UniTask<string> LoadAsync();
    }
}
