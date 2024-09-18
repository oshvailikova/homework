using System;
using UnityEngine;

namespace ShootEmUp
{
    public  class HealthComponent : MonoBehaviour
    {
        public event Action OnDeath;
        public event Action<int> OnTakeDamage;

        [SerializeField]
        private int _maxHitPoints;

        private int _hitPoints;

        public void ResetHealth()
        {
            _hitPoints = _maxHitPoints;
        }

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            if (_hitPoints > 0)
            {
                OnTakeDamage?.Invoke(_hitPoints);
            }

            if (_hitPoints <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
}