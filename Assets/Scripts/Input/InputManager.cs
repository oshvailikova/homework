using System;
using UnityEngine;

namespace ShootEmUp
{ 
    public class InputManager : MonoBehaviour,
        IGameUpdateListener
    {  
        public event Action OnShootEvent;
        public event Action<Vector2> OnMoveEvent;

        private Vector2 _movementDirection;

        private void Start()
        {
            this.As<IGameListener>().Register();
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

        public void OnUpdate(float time)
        {
            HandleShoot();
            HandleMove();
        }
    }
}