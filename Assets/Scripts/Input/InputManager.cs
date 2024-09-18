using System;
using UnityEngine;

namespace ShootEmUp
{ 
    public  class InputManager : MonoBehaviour
    {  
        public event Action OnShootKeyDownEvent;
        public event Action<Vector2> OnMoveEvent;

        private Vector2 _movementDireection;
        

        private void Update()
        {
            HandleShoot();
            HandleMove();
        }

        private void HandleShoot()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnShootKeyDownEvent?.Invoke();
                //characterController._fireRequired = true;
            }
        }

        private void HandleMove()
        {
            _movementDireection = Vector2.zero;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _movementDireection += Vector2.left;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                _movementDireection+= Vector2.right;
            }
            OnMoveEvent?.Invoke(_movementDireection);
        }

    }
}