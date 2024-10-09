using ShootEmUp;
using Zenject;

namespace ShootEmUp
{
    public sealed class PlayerInputObserver :
        IGameStartListener, IGameFinishListener
    {
        private Player _player;
        private InputManager _inputManager;

        [Inject]
        public void Constract(Player player, InputManager inputManager)
        {
            _player = player;
            _inputManager = inputManager;
        }

        public void OnGameStart()
        {
            _inputManager.OnShootEvent += _player.RequireFire;
            _inputManager.OnMoveEvent += _player.SetMoveDirection;
        }

        public void OnGameFinish()
        {
            _inputManager.OnShootEvent -= _player.RequireFire;
            _inputManager.OnMoveEvent -= _player.SetMoveDirection;
        }
    }
}
