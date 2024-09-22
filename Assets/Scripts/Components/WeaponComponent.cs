using UnityEngine;

namespace ShootEmUp
{
    public class WeaponComponent : MonoBehaviour
    {
        [SerializeField]
        private Transform _firePoint;     
        [SerializeField]
        private BulletConfig _bulletConfig;

        private IBulletSpawner _bulletSpawner;

        public void Initialize(IBulletSpawner spawner)
        {
           _bulletSpawner = spawner;
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