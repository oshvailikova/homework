using System.Collections.Generic;
using Zenject;
using SaveSystem.Base;
using SaveSystem.Utils.Base;

namespace SaveSystem
{
    public class GameStateManager : IStateManager
    {
        private Dictionary<string, string> _currentGameState = new();

        private ISerializationService _serializationService;

        [Inject]
        public GameStateManager(ISerializationService serializationService)
        {
            _serializationService = serializationService;
        }

        public string GetCurrentState()
        {
            return _serializationService.Serialize(_currentGameState);
        }

        public void SetCurrentState(string state)
        {
            _currentGameState = _serializationService.Deserialize<Dictionary<string, string>>(state);
        }

        public void ClearCurrentState()
        {
            _currentGameState.Clear();
        }

        public void AddStateData<T>(string key, T value)
        {
            _currentGameState[key] = _serializationService.Serialize(value);
        }

        public bool TryGetData<T>(string key, out T value)
        {
            if (_currentGameState.TryGetValue(key, out var serializedData))
            {
                value = _serializationService.Deserialize<T>(serializedData);
                return true;
            }

            value = default;
            return false;
        }
    }
}