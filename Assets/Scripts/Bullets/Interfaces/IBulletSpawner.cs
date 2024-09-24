using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp {
    public interface IBulletSpawner
    {
        void Spawn(Transform firePoint, BulletConfig bulletConfig);
    }
}