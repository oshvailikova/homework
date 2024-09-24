using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour,
        IGameStartListener, IGameFixedUpdateListener
    {
        [SerializeField]
        private Params _params;

        private Vector2 _initialPosition;
        private float _startPositionY;
        private float _endPositionY;
        private float _movingSpeedY;
        private float _positionX;
        private float _positionZ;

        private void Awake()
        {
            _initialPosition = transform.position;

            _startPositionY = _params.StartPositionY;
            _endPositionY = _params.EndPositionY;
            _movingSpeedY = _params.MovingSpeedY;
        }

        private void Start()
        {
            this.As<IGameListener>().Register();
        }

        public void OnGameStart()
        {
            transform.position = _initialPosition;

            var position = transform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (transform.position.y <= _endPositionY)
            {
                transform.position = new Vector3(
                    _positionX,
                    _startPositionY,
                    _positionZ
                );
            }

            transform.position -= new Vector3(
                _positionX,
                _movingSpeedY * deltaTime,
                _positionZ
            );
        }

        [Serializable]
        public  class Params
        {
            [SerializeField]
            public float StartPositionY;

            [SerializeField]
            public float EndPositionY;

            [SerializeField]
            public float MovingSpeedY;
        }
    }
}