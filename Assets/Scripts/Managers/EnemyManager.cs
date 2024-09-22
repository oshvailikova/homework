using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShootEmUp
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawner _enemySpawner;
        [SerializeField]
        private float _spawnInterval = 1f;
        [SerializeField]
        private EnemyPositions _enemyPositions;
        [SerializeField]
        private Transform _aimTransform;
        [SerializeField]
        private LevelBounds _levelBounds;
        [SerializeField]
        private BulletSpawner _bulletSpawner;

        private void Start()
        {
            StartCoroutine(SpawnEnemy());
        }

        private void OnDisable()
        {
            StopSpawning();
        }

        private IEnumerator SpawnEnemy()
        {
            while (true)
            {
                _enemySpawner.SpawnEnemy(GetEnemyInfo());
                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        private EnemyInfo GetEnemyInfo()
        {
            return new EnemyInfo
            {
                SpawnTransform = _enemyPositions.RandomSpawnPosition(),
                MoveTargetTransform = _enemyPositions.RandomAttackPosition(),
                AimTransform = _aimTransform,
                LevelBounds = _levelBounds,
                BulletSpawner = _bulletSpawner
            };
        }

        private void StopSpawning()
        {
            StopAllCoroutines();
        }

    }
}