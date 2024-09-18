using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnEnemyDeath;

        [SerializeField]
        private float _shootingTime;

        private WeaponComponent _weaponComponent;
        private MovementComponent _movementComponent;
        private HealthComponent _healthComponent;

        private EnemyMovement _enemyMovement;
        private EnemyWeapon _enemyWeapon;

        public EnemyPoolObject PoolObject
        {
            get; private set;
        }

        private void OnEnable()
        {
            _healthComponent.OnDeath += EnemyDeath;
        }

        private void OnDisable()
        {
            _healthComponent.OnDeath -= EnemyDeath;
        }

        private void Awake()
        {
            _weaponComponent = GetComponent<WeaponComponent>();
            _movementComponent = GetComponent<MovementComponent>();
            _healthComponent = GetComponent<HealthComponent>();
        }

        public void Init(EnemyInfo info)
        {
            transform.position = info.SpawnTransform.position;
            _movementComponent.Initialize(info.LevelBounds);
            _weaponComponent.Initialize(info.ShootEventManager);
            _enemyMovement = new EnemyMovement(_movementComponent, transform, info.MoveTargetTransform);
            _enemyWeapon = new EnemyWeapon(_weaponComponent, _shootingTime, transform, info.AimTransform);
        }

        public void SetPoolObject(EnemyPoolObject enemyPoolObject)
        {
            PoolObject = enemyPoolObject;
        }

        private void EnemyDeath()
        {
            OnEnemyDeath.Invoke(this);
        }

        private void FixedUpdate()
        {
            _enemyMovement.UpdateMovement(Time.fixedDeltaTime);
            _enemyWeapon.UpdateWeapon(Time.fixedDeltaTime, _enemyMovement.IsTargetAchieved);
        }
    }
}