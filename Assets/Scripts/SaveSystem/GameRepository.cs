using SaveSystem.Base;
using SaveSystem.SnapshotSystem;
using SaveSystem.SnapshotSystem.Base;
using Zenject;

//TODO Change ISaveLoadSystem to array in case there are other options for saving besides the file, for example cloud saving, etc.
namespace SaveSystem
{
    public sealed class GameRepository : IGameRepository
    {        
        private readonly IStateManager _stateManager; 
        private readonly ISnapshotManager _snapshotManager;
        private readonly ISaveLoadSystem _saveSystem;

        [Inject]
        public GameRepository(IStateManager stateManager, ISnapshotManager snapshotManager, ISaveLoadSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _stateManager = stateManager;
            _snapshotManager = snapshotManager;
        }


        public void SaveState()
        {
            var currentState = _stateManager.GetCurrentState(); 
            var snapshot = GameSnapshot.FromGameState(currentState); 

            _snapshotManager.SaveSnapshot(snapshot); 
            _saveSystem.Save(currentState);
        }


        public void LoadState()
        {
            var serializedData = _saveSystem.Load(); 
            var snapshot = GameSnapshot.FromGameState(serializedData);

            _stateManager.SetCurrentState(serializedData); 
            _snapshotManager.SaveSnapshot(snapshot); 
        }


        public void RestoreLastState()
        {
            var snapshot = _snapshotManager.LoadLastSnapshot(); 
            if (snapshot != null)
            {
                _stateManager.SetCurrentState(snapshot.GetSnapshot()); 
            }
        }

        public void ClearState()
        {
           _snapshotManager.ClearSnapshots();
            _stateManager.ClearCurrentState();

            SaveState();
        }


        public bool TryGetData<T>(string key, out T value)
        {
            return _stateManager.TryGetData(key, out value); 
        }

        public void SetData<T>(string key, T value)
        {
            _stateManager.AddStateData(key, value);
        }
    }
}

