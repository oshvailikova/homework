using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class UIGameSystemController : MonoBehaviour,
        IGameStartListener, IGameFinishListener,
        IGamePauseListener, IGameResumeListener
    {
        [SerializeField]
        private ButtonController _startButtonController;

        [SerializeField]
        private ButtonController _pauseButtonController;

        [SerializeField]
        private ButtonController _resumeButtonController;


        private CountdownLauncher _countdownLauncher;

        private GameManager _gameManager;

        [Inject]
        public void Construct(GameManager gameManager, CountdownLauncher countdownLauncher)
        {
            _gameManager = gameManager;
            _countdownLauncher = countdownLauncher;

            _countdownLauncher.LauncherIsReady += OnCountdownFinished;
        }

        private void Start()
        {
            _startButtonController.AddClickListener(OnStartButtonClicked);
            _pauseButtonController.AddClickListener(OnPauseButtonClicked);
            _resumeButtonController.AddClickListener(OnResumeButtonClicked);

            _startButtonController.Show();
            _pauseButtonController.Hide();
            _resumeButtonController.Hide();
        }

        private void OnDestroy()
        {
            _startButtonController.RemoveListeners();
            _pauseButtonController.RemoveListeners();
            _resumeButtonController.RemoveListeners();
        }

        private void OnStartButtonClicked()
        {
            _startButtonController.Hide();
            _pauseButtonController.Hide();
            _resumeButtonController.Hide();
            _countdownLauncher.StartCountdown();
        }

        private void OnCountdownFinished()
        {
            _gameManager.OnStart();
        }

        private void OnPauseButtonClicked()
        {
            _gameManager.OnPause();
        }

        private void OnResumeButtonClicked()
        {
            _gameManager.OnResume();
        }

        public void OnGameStart()
        {
            _pauseButtonController.Show();
            _startButtonController.Hide();
            _resumeButtonController.Hide();
        }

        public void OnGamePause()
        {
            _pauseButtonController.Hide();
            _resumeButtonController.Show();
        }

        public void OnGameResume()
        {
            _pauseButtonController.Show();
            _resumeButtonController.Hide();
        }

        public void OnGameFinish()
        {
            _pauseButtonController.Hide();
            _resumeButtonController.Hide();
            _startButtonController.Show();
        }
    }
}