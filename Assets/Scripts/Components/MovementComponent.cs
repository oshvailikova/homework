using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    public  class MovementComponent : MonoBehaviour
    {       
        [SerializeField]
        private float _speed = 5.0f;

        private LevelBounds _levelBounds;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _velocity;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _velocity = Vector2.zero;
        }

        public void Initialize(LevelBounds levelBounds)
        {
            _levelBounds = levelBounds;
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