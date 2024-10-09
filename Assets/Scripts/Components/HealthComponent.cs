using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HealthComponent
    {
        public event Action OnDeath;

        private int _maxHitPoints;
        private int _hitPoints;

        public HealthComponent(int health)
        {
            _maxHitPoints = health;
        }

        public void ResetHealth()
        {
            _hitPoints = _maxHitPoints;
        }

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            
            if (_hitPoints <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
}