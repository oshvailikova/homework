using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class Timer
    {
        private float _remainingTime;
        private float _countdownTime;

        public bool IsReady { get; private set; }

        public Timer(float countDownTime)
        {
            _countdownTime = countDownTime;
            Reset();
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
            }
        }
    }
}