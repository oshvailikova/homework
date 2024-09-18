using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSpawner : InitilizableObject
    {
        [SerializeField]
        private BulletPool _pool;

        public override void Initialize(LevelBounds levelBounds, ShootEventManager shootEventManager) 
        {
            base.Initialize(levelBounds, shootEventManager);
            _shootEventManager?.SubscribeToShoot(SpawnBullet);
        }

        void OnDisable()
        {
            _shootEventManager?.UnsubscribeFromShoot(SpawnBullet);
        }

        public void SpawnBullet(Transform firePoint, BulletConfig bulletConfig)
        {            
            var bullet = _pool.Pop(new BulletInfo
            {
                Position = firePoint.position,
                Direction = firePoint.rotation * Vector3.up,
                BulletConfig = bulletConfig,
                LevelBounds = _levelBounds
            }).Bullet;
            bullet.OnDestroyBullet += ReturnBullet;
        }

        public void ReturnBullet(Bullet bullet)
        {
            bullet.OnDestroyBullet -= ReturnBullet;
            _pool.Push(bullet.PoolObject);
        }
    }
}