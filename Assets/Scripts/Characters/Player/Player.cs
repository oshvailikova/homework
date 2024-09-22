using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MovementComponent), typeof(HealthComponent), typeof(WeaponComponent))]
    public class Player : Character<Player>
    {
        [SerializeField]
        private LevelBounds _levelBounds;
        [SerializeField]
        private BulletSpawner _bulletSpawner;

        private Vector2 _moveDirection;

        protected override void Awake()
        {
            base.Awake();

            _movementComponent.Initialize(_levelBounds);
            _weaponComponent.Initialize(_bulletSpawner);
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

    }
}