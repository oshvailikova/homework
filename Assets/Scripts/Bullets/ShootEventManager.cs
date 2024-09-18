using ShootEmUp;
using System;
using UnityEngine;

public sealed class ShootEventManager : MonoBehaviour
{
    public event Action<Transform, BulletConfig> OnShoot;

    public void TriggerShoot(Transform firePoint, BulletConfig bulletConfig)
    {
        OnShoot?.Invoke(firePoint,bulletConfig); 
    }

    public void SubscribeToShoot(Action<Transform, BulletConfig> listener)
    {
        OnShoot += listener;
    }

    public void UnsubscribeFromShoot(Action<Transform, BulletConfig> listener)
    {
        OnShoot -= listener;
    }
}
