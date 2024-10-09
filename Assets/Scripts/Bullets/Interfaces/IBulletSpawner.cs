using System;
using UnityEngine;

namespace ShootEmUp {
    public interface IBulletSpawner
    {
        public static event Action<Bullet> OnRegister;
        public static event Action<Bullet> OnRemove;

        public void Register(Bullet bullet)
        {
            OnRegister?.Invoke(bullet);
        }

        public void Remove(Bullet bullet)
        {
            OnRemove?.Invoke(bullet);
        }
        public void Spawn(Transform firePoint, BulletConfig bulletConfig);

        public void Despawn(Bullet bullet);
    }
}