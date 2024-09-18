using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public  class EnemyMovement
    {
        private const float MIN_DISTANCE = 0.25f;

        private MovementComponent _moveComponent;
        private Transform _selfTransform;
        private Transform _moveTarget;

        public bool IsTargetAchieved { get; private set; }

        public EnemyMovement(MovementComponent movementComponent, Transform selfTransform, Transform moveTarget)
        {
            _moveComponent = movementComponent;           
            _selfTransform = selfTransform;
            _moveTarget = moveTarget;
        }

        public void UpdateMovement(float time)
        {
            if (IsTargetAchieved) return;
            var vector = _moveTarget.position - _selfTransform.position;
            if (vector.magnitude <= MIN_DISTANCE)
            {
                IsTargetAchieved = true;
                return;
            }

            var direction = vector.normalized * time;
            _moveComponent.Move(direction);
        }
    }
}