using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "EnemyConfig",
        menuName = "Enemy/New EnemyConfig"
    )]
    public sealed class EnemyConfig : ScriptableObject
    {
        [SerializeField]
        private int _shootingTime;
        [SerializeField]
        private int _health;
        [SerializeField]
        private int _speed;
        [SerializeField]
        private BulletConfig _bulletConfig;

        public int ShootingTime
        {
            get => _shootingTime;
        }

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