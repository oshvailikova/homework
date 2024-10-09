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
        private int _spawnInterval = 1;
        [SerializeField]
        private int _maxEnemyCount = 10;

        public int SpawnIntervalInMillis
        {
            get => _spawnInterval * 1000;
        }

        public int MaxEnemyCount
        {
            get => _maxEnemyCount;
        }

    }
}