using SaveSystem.SnapshotSystem.Base;

namespace SaveSystem.SnapshotSystem
{
    public class GameSnapshot : ISnapshot
    {
        private string _state;

        public GameSnapshot(string gameState)
        {
            _state = gameState;
        }

        public string GetSnapshot() => _state;

        public void SetSnapshot(string state)
        {
            _state = state;
        }

        public static GameSnapshot FromGameState(string gameState)
        {
            return new GameSnapshot(gameState);
        }

    }
}
