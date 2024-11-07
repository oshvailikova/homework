using SaveSystem.Base;
using SaveSystem.Data.Base;

namespace SaveSystem.Data
{
    public abstract class DataHandler<TData> : IDataHandler
    {
        protected abstract string Key { get; }

        public void LoadGame(IGameRepository repository)
        {    
            if (repository.TryGetData(Key, out TData data))
            {
                SetupData(data);
            }
            else
            {
                SetupDefaultData();
            }
        }

        public void SaveGame(IGameRepository repository)
        {
            TData data = ConvertToData();
            repository.SetData(Key, data);
        }

        protected abstract TData ConvertToData();
        protected abstract void SetupData(TData data);
        protected virtual void SetupDefaultData()
        {
        }
    }
}