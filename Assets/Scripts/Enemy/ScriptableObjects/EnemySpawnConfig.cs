using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "EnemySpawnConfig",
        menuName = "Enemy/New EnemySpawnConfig"
    )]
    public sealed class EnemySpawnConfig : ScriptableObject
    {
        [SerializeField]
        private float _spawnInterval = 1;
        [SerializeField]
        private int _maxEnemyCount = 10;

        public float SpawnInterval
        {
            get => _spawnInterval;
        }

        public int MaxEnemyCount
        {
            get => _maxEnemyCount;
        }

    }
}