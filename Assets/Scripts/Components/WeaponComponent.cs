using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent
    {
        private Transform _firePoint;
        private BulletConfig _bulletConfig;
        private IBulletSpawner _bulletSpawner;

        public WeaponComponent(Transform firePoint, IBulletSpawner bulletSpawner, BulletConfig bulletConfig)
        {
            _firePoint = firePoint;
            _bulletSpawner = bulletSpawner;
            _bulletConfig = bulletConfig;
        }

        public void Shoot()
        {
            _bulletSpawner.Spawn(_firePoint, _bulletConfig);
        }

        public void Shoot(Vector2 direction)
        {
            _firePoint.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            _bulletSpawner.Spawn(_firePoint, _bulletConfig);
        }
    }
}