using SaveSystem.Base;

namespace SaveSystem.Data.Base
{
    public interface IDataHandler
    {
        void SaveGame(IGameRepository gameRepository);
        void LoadGame(IGameRepository gameRepository);
    }
}