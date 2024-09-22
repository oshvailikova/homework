using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character<T> : MonoBehaviour, IDestroyable<T> where T:Component
{
    public event Action<T> OnDestroy;

    protected WeaponComponent _weaponComponent;
    protected MovementComponent _movementComponent;
    protected HealthComponent _healthComponent;

    protected virtual void Awake()
    {
        _weaponComponent = GetComponent<WeaponComponent>();
        _movementComponent = GetComponent<MovementComponent>();
        _healthComponent = GetComponent<HealthComponent>();
    }

    private void OnEnable()
    {
       _healthComponent.OnDeath += Destroy;
    }

    private void OnDisable()
    {
       _healthComponent.OnDeath -= Destroy;
    }

    public void Destroy()
    {
        OnDestroy.Invoke(this as T);
    }
}
