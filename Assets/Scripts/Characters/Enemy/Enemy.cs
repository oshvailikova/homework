using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnDestroy;

        [SerializeField]
        private float _shootingTime;

        private WeaponComponent _weaponComponent;
        private MovementComponent _movementComponent;
        private HealthComponent _healthComponent;

        private EnemyMovement _enemyMovement;
        private EnemyWeapon _enemyWeapon;

        private void Awake()
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

        private void FixedUpdate()
        {
            _enemyMovement.UpdateMovement(Time.fixedDeltaTime);

            if (_enemyMovement.HasReachedTarget)
            {
                _enemyWeapon.UpdateWeapon(Time.fixedDeltaTime);
            }
        }

        public void Init(EnemyInfo info)
        {
             transform.position = info.SpawnTransform.position;
            _movementComponent.Initialize(info.LevelBounds);
            _weaponComponent.Initialize(info.BulletSpawner);
            _enemyMovement = new EnemyMovement(_movementComponent, transform, info.MoveTargetTransform);
            _enemyWeapon = new EnemyWeapon(_weaponComponent, _shootingTime, transform, info.AimTransform);
        }

        private void Destroy()
        {
            OnDestroy.Invoke(this);
        }
    }
}