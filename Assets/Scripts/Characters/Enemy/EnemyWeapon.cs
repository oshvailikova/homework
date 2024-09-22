using Common;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyWeapon
    {
        private WeaponComponent _weaponComponent;
        private Transform _aimTransform;
        private Transform _selfTransform;

        private Timer _timer;

        public EnemyWeapon(WeaponComponent weaponComponent, float shootingCooldownTimer, Transform selfTransform, Transform aimTransform)
        {
            _weaponComponent = weaponComponent;
            _selfTransform = selfTransform;
            _aimTransform = aimTransform;
            _timer = new Timer(shootingCooldownTimer);
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