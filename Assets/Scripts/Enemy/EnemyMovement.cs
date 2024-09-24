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

        public bool HasReachedTarget { get; private set; }

        public EnemyMovement(MovementComponent movementComponent, Transform selfTransform, Transform moveTarget)
        {
            _moveComponent = movementComponent;           
            _selfTransform = selfTransform;
            _moveTarget = moveTarget;
        }

        public void UpdateMovement(float deltaTime)
        {
            if (Vector3.Distance(_selfTransform.position, _moveTarget.position) <= MIN_DISTANCE)
            {
                HasReachedTarget = true;
            }
            else
            {
                HasReachedTarget = false;
                _moveComponent.Move((_moveTarget.position - _selfTransform.position).normalized * deltaTime);
            }
        }
    }
}