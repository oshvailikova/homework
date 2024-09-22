using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class Enemy : Character<Enemy>
    {
        [SerializeField]
        private float _shootingTime;

        private EnemyMovement _enemyMovement;
        private EnemyWeapon _enemyWeapon;

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

    }
}