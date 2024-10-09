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
            CountdownCoroutine();
        }

        private void OnCountdownFinished()
        {
            _countdownDisplay.SetVisibility(false);
            LauncherIsReady?.Invoke();
        }

        private async void CountdownCoroutine()
        {
            while (!_timer.IsReady)
            {
                _countdownDisplay.UpdateDisplay(_timer.GetRemainingTime());

                await UniTask.Delay(1000);

                _timer.Update(1f);
            }
        }

    }
}
