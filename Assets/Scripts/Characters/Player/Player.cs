using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MovementComponent), typeof(HealthComponent), typeof(WeaponComponent))]
    public class Player: MonoBehaviour
    {
        public event Action<Player> OnDestroy;

        [SerializeField]
        private LevelBounds _levelBounds;
        [SerializeField]
        private BulletSpawner _bulletSpawner;

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
            var direction = _moveDirection.normalized * Time.fixedDeltaTime;
            _movementComponent.Move(direction);
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
    }
}