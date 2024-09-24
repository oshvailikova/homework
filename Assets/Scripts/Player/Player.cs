using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MovementComponent), typeof(HealthComponent), typeof(WeaponComponent))]
    public class Player : MonoBehaviour,
    IGameStartListener, IGameFinishListener,
    IGameFixedUpdateListener
    {
        public event Action<Player> OnDestroy;

        [SerializeField]
        private LevelBounds _levelBounds;
        [SerializeField]
        private BulletSpawner _bulletSpawner;

        private Vector2 _startPosition;

        private WeaponComponent _weaponComponent;
        private MovementComponent _movementComponent;
        private HealthComponent _healthComponent;

        private Vector2 _moveDirection;

        private void Awake()
        {
            _weaponComponent = GetComponent<WeaponComponent>();
            _movementComponent = GetComponent<MovementComponent>();
            _healthComponent = GetComponent<HealthComponent>();

            _movementComponent.Initialize(_levelBounds);
            _weaponComponent.Initialize(_bulletSpawner);
        }

        private void Start()
        {
            this.As<IGameListener>().Register();
        }

        public void RequireFire()
        {
            _weaponComponent.Shoot();
        }

        public void SetMoveDirection(Vector2 direction)
        {
            _moveDirection = direction;
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