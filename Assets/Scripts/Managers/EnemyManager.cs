using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShootEmUp
{
    public  class EnemyManager : InitilizableObject
    {
        [SerializeField]
        private EnemySpawner _enemySpawner;
        [SerializeField]
        private float _spawnInterval = 1f;
        [SerializeField]
        private EnemyPositions _enemyPositions;
        [SerializeField]
        private Transform _aimTransform;

        public override void Initialize(LevelBounds levelBounds, ShootEventManager shootEventManager)
        {
            base.Initialize(levelBounds, shootEventManager);
            StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            while (true)
            {
                _enemySpawner.SpawnEnemy(CreateEnemySpawnParams());
                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        private EnemyInfo CreateEnemySpawnParams()
        {
            return new EnemyInfo
            {
                SpawnTransform = _enemyPositions.RandomSpawnPosition(),
                MoveTargetTransform = _enemyPositions.RandomAttackPosition(),
                AimTransform = _aimTransform,
                LevelBounds = _levelBounds,
                ShootEventManager = _shootEventManager
            };
        }

        public void StopSpawning()
        {
            StopAllCoroutines();
        }

        private void OnDisable()
        {
            StopSpawning();
        }

    }
}