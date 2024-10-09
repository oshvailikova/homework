using UnityEngine;

namespace ShootEmUp
{
    public sealed class MovementComponent 
    {   
        private float _speed = 5.0f;

        private LevelBounds _levelBounds;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _velocity;

        public MovementComponent(Rigidbody2D rigidbody2D, LevelBounds levelBounds)
        {
            _levelBounds = levelBounds;
            _rigidbody2D = rigidbody2D;
            _velocity = Vector2.zero;
        }

        public void Init(int speed)
        {
            _velocity = Vector2.zero;
            _speed = speed;
        }

        public void Move(Vector2 direction)
        {
            _velocity = direction * _speed;
            var nextPosition = _rigidbody2D.position + _velocity;
            if (_levelBounds.InBounds(nextPosition))
                _rigidbody2D.MovePosition(nextPosition);
        }

    }
}