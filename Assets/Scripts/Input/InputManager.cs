using System;
using UnityEngine;

namespace ShootEmUp
{ 
    public class InputManager : MonoBehaviour
    {  
        public event Action OnShootEvent;
        public event Action<Vector2> OnMoveEvent;

        private Vector2 _movementDirection;
        
        private void Update()
        {
            HandleShoot();
            HandleMove();
        }

        private void HandleShoot()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnShootEvent?.Invoke();
            }
        }

        private void HandleMove()
        {
            _movementDirection = Vector2.zero;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _movementDirection = Vector2.left;
            }      
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                _movementDirection = Vector2.right;
            }
            
            OnMoveEvent?.Invoke(_movementDirection);
        }

    }
}