using Cysharp.Threading.Tasks;

namespace SaveSystem.Base
{
    public interface IFileStorage
    {
        void Write(string data);
        string Read();

        UniTask WriteAsync(string data);
        UniTask<string> ReadAsync();
    }
}