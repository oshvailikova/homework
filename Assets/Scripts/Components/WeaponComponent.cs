using UnityEngine;

namespace ShootEmUp
{
    public class WeaponComponent : MonoBehaviour
    {
        [SerializeField]
        private Transform _firePoint;     
        [SerializeField]
        private BulletConfig _bulletConfig;

        private ShootEventManager _shootEventManager;
        
        public void Initialize(ShootEventManager shootEventManager)
        {
            _shootEventManager = shootEventManager;
        }
        
        public void Shoot()
        {
            _shootEventManager.TriggerShoot(_firePoint, _bulletConfig);
        }

        public void Shoot(Vector2 direction)
        {
            _firePoint.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            _shootEventManager.TriggerShoot(_firePoint, _bulletConfig);
        }
    }
}