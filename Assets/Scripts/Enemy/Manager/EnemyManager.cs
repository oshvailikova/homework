using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShootEmUp
{
    public class EnemyManager : MonoBehaviour,
        IGameStartListener, IGamePauseListener, IGameResumeListener, IGameFinishListener
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
            this.As<IGameListener>().Register();
        }

        private void StartSpawning()
        {
            StartCoroutine(SpawnEnemy());
        }

        private void StopSpawning()
        {
            StopAllCoroutines();
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

        public void OnGameStart()
        {
            StartSpawning();
        }

        public void OnGamePause()
        {
            StartSpawning();
        }

        public void OnGameResume()
        {
            StartSpawning();
        }

        public void OnGameFinish()
        {
            StopSpawning();
            _enemySpawner.Clear();
        }
    }
}