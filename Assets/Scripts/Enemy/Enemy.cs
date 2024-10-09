using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class Enemy : MonoBehaviour, IDestructible
    {
        public event Action<Enemy> OnDestroy;

        [SerializeField]
        private Transform _firePoint;
        [SerializeField]
        private EnemyConfig _enemyConfig;

        private EnemyMovement _enemyMovement;
        private EnemyWeapon _enemyWeapon;

        private WeaponComponent _weaponComponent;
        private MovementComponent _movementComponent;
        private HealthComponent _healthComponent;

        [Inject]
        public void Construct(LevelBounds levelBounds, IBulletSpawner bulletSpawner)
        {
            var rigidbody2D = GetComponent<Rigidbody2D>();

            _movementComponent = new MovementComponent(rigidbody2D, levelBounds);
            _healthComponent = new HealthComponent(_enemyConfig.Health);
            _weaponComponent = new WeaponComponent(_firePoint, bulletSpawner, _enemyConfig.BulletConfig);

            _enemyMovement = new EnemyMovement(_movementComponent, transform);
            _enemyWeapon = new EnemyWeapon(_weaponComponent, _enemyConfig.ShootingTime, transform);

        }

        public void Init(EnemyInfo info)
        {
            transform.position = info.SpawnTransform.position;

            _movementComponent.Init(_enemyConfig.Speed);

            _healthComponent.ResetHealth();

            _enemyMovement.SetMoveTarget(info.MoveTargetTransform);
            _enemyWeapon.SetAim(info.AimTransform);

            _healthComponent.OnDeath += Destroy;
        }

        public void TakeDamage(int damage)
        {
            _healthComponent.TakeDamage(damage);
        }

        private void Destroy()
        {
            _healthComponent.OnDeath -= Destroy;
            OnDestroy.Invoke(this);
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