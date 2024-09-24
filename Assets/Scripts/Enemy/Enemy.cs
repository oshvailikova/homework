using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class Enemy : MonoBehaviour,
         IGameStartListener, IGameFinishListener,
         IGameFixedUpdateListener
    {
        public event Action<Enemy> OnDestroy;

        [SerializeField]
        private float _shootingTime;

        private EnemyMovement _enemyMovement;
        private EnemyWeapon _enemyWeapon;

        private WeaponComponent _weaponComponent;
        private MovementComponent _movementComponent;
        private HealthComponent _healthComponent;

        private void Awake()
        {
            _weaponComponent = GetComponent<WeaponComponent>();
            _movementComponent = GetComponent<MovementComponent>();
            _healthComponent = GetComponent<HealthComponent>();
        }

        private void Start()
        {
            this.As<IGameListener>().Register();
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
            this.As<IGameListener>().Remove();
            OnDestroy.Invoke(this);
        }

        public void OnGameStart()
        {
            _healthComponent.ResetHealth();
            _healthComponent.OnDeath += Destroy;
        }

        public void OnGameFinish()
        {
            _healthComponent.OnDeath -= Destroy;
        }

        public void OnFixedUpdate(float fixedTime)
        {
            _enemyMovement.UpdateMovement(fixedTime);

            if (_enemyMovement.HasReachedTarget)
            {
                _enemyWeapon.UpdateWeapon(fixedTime);
            }
        }

    }
}