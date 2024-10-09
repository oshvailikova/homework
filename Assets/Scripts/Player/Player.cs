using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class Player : MonoBehaviour, IDestructible,
    IGameStartListener, IGameFinishListener,
    IGameFixedUpdateListener
    {
        public event Action<Player> OnDestroy;

        [SerializeField]
        private Transform _firePoint;

        private PlayerConfig _playerConfig;

        private WeaponComponent _weaponComponent;
        private MovementComponent _movementComponent;
        private HealthComponent _healthComponent;

        private Vector2 _startPosition;
        private Vector2 _moveDirection;

        [Inject]
        public void Construct(LevelBounds levelBounds, IBulletSpawner bulletSpawner, PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;

            var rigidBody2D = GetComponent<Rigidbody2D>();

            _weaponComponent = new WeaponComponent(_firePoint, bulletSpawner, _playerConfig.BulletConfig);
            _movementComponent = new MovementComponent(rigidBody2D, levelBounds);
            _healthComponent = new HealthComponent(_playerConfig.Health);

            _movementComponent.Init(_playerConfig.Speed);

        }

        public void RequireFire()
        {
            _weaponComponent.Shoot();
        }

        public void SetMoveDirection(Vector2 direction)
        {
            _moveDirection = direction;
        }

        public void TakeDamage(int damage)
        {
            _healthComponent.TakeDamage(damage);
        }
        private void Destroy()
        {
            OnDestroy.Invoke(this);
        }

        public void OnGameStart()
        {
            _startPosition = transform.position;

            _healthComponent.ResetHealth();
            _healthComponent.OnDeath += Destroy;
        }

        public void OnGameFinish()
        {
            transform.position = _startPosition;

            _healthComponent.OnDeath -= Destroy;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            var direction = _moveDirection.normalized * deltaTime;
            _movementComponent.Move(direction);
        }
    }
}