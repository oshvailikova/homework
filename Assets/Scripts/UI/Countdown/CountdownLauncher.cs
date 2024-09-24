using Common;
using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace ShootEmUp
{
    public class CountdownLauncher : MonoBehaviour
    {
        public event Action LauncherIsReady;

        [SerializeField]
        private float _countdownStartValue;

        [SerializeField]
        private CountdownDisplay _countdownDisplay;

        private Timer _timer;

        private void Awake()
        {
            _timer = new Timer(_countdownStartValue);
            _timer.OnFinished += OnCountdownFinished;
        }

        public void StartCountdown()
        {
            _timer.Reset();
            _countdownDisplay.SetVisibility(true);
            StartCoroutine(CountdownCoroutine());
        }

        private IEnumerator CountdownCoroutine()
        {
            while (!_timer.IsReady)
            {
                _countdownDisplay.UpdateDisplay(_timer.GetRemainingTime());

                yield return new WaitForSeconds(1f);

                _timer.Update(1f);
            }
        }

        private void OnCountdownFinished()
        {
            _countdownDisplay.SetVisibility(false);
            LauncherIsReady?.Invoke();
        }
    }
}
