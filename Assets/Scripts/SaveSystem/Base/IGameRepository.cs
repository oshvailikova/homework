using Cysharp.Threading.Tasks;

namespace SaveSystem.Base
{
    public interface IGameRepository
    {
        void LoadState();

        void SaveState();

        void RestoreLastState();

        void ClearState();

        bool TryGetData<T>(string key, out T value);

        void SetData<T>(string key, T value);
    }
}