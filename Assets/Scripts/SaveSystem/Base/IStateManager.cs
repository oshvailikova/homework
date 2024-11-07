namespace SaveSystem.Base
{
    public interface IStateManager
    {
        public string GetCurrentState();

        public void SetCurrentState(string state);

        public void ClearCurrentState();

        public void AddStateData<T>(string key, T value);

        public bool TryGetData<T>(string key, out T value);
    }
}