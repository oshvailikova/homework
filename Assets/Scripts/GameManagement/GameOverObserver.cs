using Zenject;

namespace ShootEmUp
{
    public sealed class GameOverObserver :
        IGameStartListener,IGameFinishListener
    {
        private GameManager _gameManager;
        private Player _player;

        [Inject]
        public void Construct(GameManager gameManager, Player player)
        {
            _gameManager = gameManager;
            _player = player;
        }

        private void FinishGame(Player player)
        {
            _gameManager.OnFinish();
        }

        public void OnGameStart()
        {
            _player.OnDestroy += FinishGame;
        }

        public void OnGameFinish()
        {
            _player.OnDestroy -= FinishGame;
        }
    }
}