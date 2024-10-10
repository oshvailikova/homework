using Common;
using Cysharp.Threading.Tasks;
using System;
using Zenject;

namespace ShootEmUp
{
    public sealed class CountdownLauncher 
    {
        public event Action LauncherIsReady;

        private CountdownLauncherConfig _countdownLauncherConfig;
        private CountdownDisplay _countdownDisplay;

        private Timer _timer;

        [Inject]
        private void Construct(CountdownDisplay countdownDisplay, CountdownLauncherConfig countdownLauncherConfig)
        {
            _countdownDisplay = countdownDisplay;
            _countdownLauncherConfig = countdownLauncherConfig;

            _timer = new Timer(_countdownLauncherConfig.CountdownValue);
            _timer.OnFinished += OnCountdownFinished;
        }

        public void StartCountdown()
        {
            _timer.Reset();
            _countdownDisplay.SetVisibility(true);
            CountdownCoroutine().Forget();
        }

        private void OnCountdownFinished()
        {
            _countdownDisplay.SetVisibility(false);
            LauncherIsReady?.Invoke();
        }

        private async UniTaskVoid CountdownCoroutine()
        {
            while (!_timer.IsReady)
            {
                _countdownDisplay.UpdateDisplay(_timer.GetRemainingTime());

                await UniTask.WaitForSeconds(1.0f);

                _timer.Update(1f);
            }
        }

    }
}
