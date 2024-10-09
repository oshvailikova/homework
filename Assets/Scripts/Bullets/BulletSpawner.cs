using ShootEmUp;
using Extensions;
using UnityEngine;
using Zenject;

public class BulletSpawner : IBulletSpawner
{ 
    private BulletPool _bulletPool;

    [Inject]
    public void Construct(BulletPool bulletPool)
    {
        _bulletPool = bulletPool;
    }

    public void Spawn(Transform firePoint, BulletConfig bulletConfig)
    {
        var bullet = _bulletPool.Spawn(new BulletInfo
        {
            Position = firePoint.position,
            Direction = firePoint.rotation * Vector3.up,
            BulletConfig = bulletConfig
        });
        bullet.OnDestroy += Despawn;

        this.As<IBulletSpawner>().Register(bullet);
    }

    public void Despawn(Bullet bullet)
    {
        bullet.OnDestroy -= Despawn;
        _bulletPool.Despawn(bullet);

        this.As<IBulletSpawner>().Remove(bullet);
    }
}
