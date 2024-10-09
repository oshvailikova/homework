using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "PlayerConfig",
        menuName = "Player/New PlayerConfig"
    )]
    public sealed class PlayerConfig : ScriptableObject
    {     
        [SerializeField]
        private int _health;
        [SerializeField]
        private int _speed;
        [SerializeField]
        private BulletConfig _bulletConfig;

        public int Health
        {
            get => _health;
        }

        public int Speed
        {
            get => _speed;
        }

        public BulletConfig BulletConfig
        {
            get => _bulletConfig;
        }
    }
}