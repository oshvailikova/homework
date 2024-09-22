using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSpawner : MonoBehaviour, IBulletSpawner
    {
        [SerializeField]
        private ObjectPool _pool;
        [SerializeField]
        private LevelBounds _levelBounds;

        public void Spawn(Transform firePoint, BulletConfig bulletConfig)
        {
            var bullet = _pool.GetFromPool<Bullet>();
            bullet.Init(new BulletInfo
            {
                Position = firePoint.position,
                Direction = firePoint.rotation * Vector3.up,
                BulletConfig = bulletConfig,
                LevelBounds = _levelBounds
            });
            bullet.OnDestroy += Return;
        }

        private void Return(Bullet bullet)
        {
            bullet.OnDestroy -= Return;
            _pool.ReturnToPool(bullet.gameObject);
        }
    }

    public struct BulletInfo
    {
        public Vector2 Position;
        public bool Player;
        public Vector2 Direction;
        public BulletConfig BulletConfig;
        public LevelBounds LevelBounds;
    }
}