using Common;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyWeapon
    {
        private WeaponComponent _weaponComponent;
        private Transform _aimTransform;
        private Transform _selfTransform;

        private Timer _timer;

        public EnemyWeapon(WeaponComponent weaponComponent, float shootingCooldownTimer, Transform selfTransform)
        {
            _weaponComponent = weaponComponent;
            _selfTransform = selfTransform;
          
            _timer = new Timer(shootingCooldownTimer);
        }

        public void SetAim(Transform aimTransform)
        {
            _aimTransform = aimTransform;
            _timer.Reset();
        }

        public void UpdateWeapon(float fixedDeltaTime)
        {
            _timer.Update(fixedDeltaTime);

            if (_timer.IsReady)
            {
                var direction = _aimTransform.position - _selfTransform.position;
                _weaponComponent.Shoot(direction.normalized);
                _timer.Reset();
            }
        }
    }
}