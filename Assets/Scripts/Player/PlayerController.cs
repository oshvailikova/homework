using System;
using UnityEngine;

namespace ShootEmUp
{

    [RequireComponent(typeof(MovementComponent),typeof(HealthComponent),typeof(WeaponComponent))]
    public  class PlayerController : InitilizableObject
    {
        public event Action OnPlayerDeath;


        private HealthComponent _playerHealth;
        private MovementComponent _playerMovement;
        private WeaponComponent _playerWeapon;

        private bool _canMove = false;
        private bool _isFireRequired = false;
        private Vector2 _moveDirection;

        private void OnEnable()
        {
            _playerHealth.OnDeath += PlayerDeath;
        }

        private void Awake()
        {
            _playerHealth = GetComponent<HealthComponent>();
            _playerMovement = GetComponent<MovementComponent>();
            _playerWeapon = GetComponent<WeaponComponent>();
        }

        public override void Initialize(LevelBounds levelBounds, ShootEventManager shootEventManager)
        {
            base.Initialize(levelBounds, shootEventManager);
            _playerHealth.ResetHealth();
            _playerMovement.Initialize(levelBounds);
            _playerWeapon.Initialize(shootEventManager);
            _canMove = true;
        }

        private void PlayerDeath()
        {
            OnPlayerDeath?.Invoke();
        }

        public void RequireFire()
        {
            _isFireRequired = true;
        }

        public void SetMoveDirection(Vector2 direction)
        {
            _moveDirection = direction;
        }


        private void FixedUpdate()
        {
            if (!_canMove)
                return;
            if (_isFireRequired)
            {
                _playerWeapon.Shoot();
                _isFireRequired = false;
            }
            if (_levelBounds.InBounds(transform.position))
            {
                var direction = _moveDirection.normalized * Time.fixedDeltaTime;
                _playerMovement.Move(direction);
            }
        }

        private void OnDisable()
        {
            _playerHealth.OnDeath -= PlayerDeath;
        }

    }
}