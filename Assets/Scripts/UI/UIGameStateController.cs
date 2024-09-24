using Common;
using UnityEngine;
using UnityEngine.UI;


namespace ShootEmUp
{
    public class UIGameSystemController : MonoBehaviour,
        IGameStartListener, IGameFinishListener,
        IGamePauseListener, IGameResumeListener
    {
        [SerializeField]
        private ButtonController _startButtonController;

        [SerializeField]
        private ButtonController _pauseButtonController;

        [SerializeField]
        private ButtonController _resumeButtonController;

        [SerializeField]
        private CountdownLauncher _countdownLauncher;

        [SerializeField]
        private GameManager _gameManager;

        private void Awake()
        {
            _startButtonController.AddClickListener(OnStartButtonClicked);
            _pauseButtonController.AddClickListener(OnPauseButtonClicked);
            _resumeButtonController.AddClickListener(OnResumeButtonClicked);

            _countdownLauncher.LauncherIsReady += OnCountdownFinished;
        }

        private void Start()
        {
            this.As<IGameListener>().Register();

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
            Debug.Log("OnGameFinish");
            _pauseButtonController.Hide();
            _resumeButtonController.Hide();
            _startButtonController.Show();
        }
    }
}