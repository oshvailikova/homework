using System;
using UnityEngine;

namespace Common
{
    public sealed class Timer
    {
        public event Action OnFinished;

        private float _remainingTime;
        private float _countdownTime;

        public bool IsReady { get; private set; }

        public Timer(float countDownTime)
        {
            _countdownTime = countDownTime;
            Reset();
        }

        public float GetRemainingTime()
        {
            return Mathf.Max(0, _remainingTime);
        }

        public void Reset()
        {
            _remainingTime = _countdownTime;
            IsReady = false;
        }

        public void Update(float time)
        {
            if (IsReady)
                return;

            _remainingTime -= time;            
            if (_remainingTime <= 0)
            {              
                IsReady = true;
                OnFinished?.Invoke();
            }
        }
    }
}