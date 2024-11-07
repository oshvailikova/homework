using SaveSystem.Base;
using SaveSystem.Data.Base;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace SaveSystem
{
    public sealed class SaveLoadManager: MonoBehaviour
    {
        private IGameRepository _repository;
        private IDataHandler[] _dataHandlers;

        [Inject]
        public void Construct(IGameRepository repository, IDataHandler[] dataHandlers)
        {
            _dataHandlers = dataHandlers;
            _repository = repository;
        }

        [Button]
        public void Load()
        {
            _repository.LoadState();

            foreach (var dataHandler in _dataHandlers)
                dataHandler.LoadGame(_repository);
        }

        [Button]
        public void Save()
        {
            foreach (var dataHandler in _dataHandlers)
                dataHandler.SaveGame(_repository);

            _repository.SaveState();
        }

        [Button]
        public void RestoreLast()
        {
            _repository.RestoreLastState();

            foreach (var dataHandler in _dataHandlers)
               dataHandler.LoadGame(_repository);
        }

        [Button]
        public void Clear()
        {
            _repository.ClearState();
        }
    }
}