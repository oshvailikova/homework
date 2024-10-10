using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyManager : 
        IGameStartListener, IGamePauseListener, IGameResumeListener, IGameFinishListener,
         IGameFixedUpdateListener
    {
        private EnemySpawner _enemySpawner;
      
        private EnemyPositions _enemyPositions;
        private Transform _aimTransform;
 
        private readonly List<Enemy> _activeEnemies = new();

        private CancellationTokenSource  _spawnCancellation;

        private float _spawnInterval;

        [Inject]
        public void Construct(EnemyPool enemyPool,EnemyPositions enemyPositions, Player player, EnemySpawnConfig enemySpawnConfig)
        {
            _enemyPositions = enemyPositions;
            _aimTransform = player.transform;

            _spawnInterval = enemySpawnConfig.SpawnInterval;

            _enemySpawner = new EnemySpawner(enemySpawnConfig.MaxEnemyCount, enemyPool);
        }

        private void StartSpawning()
        {
            _spawnCancellation = new CancellationTokenSource();

            Spawn().Forget();
        }

        private void StopSpawning()
        {
            _spawnCancellation.Cancel();
        }

        private async UniTaskVoid Spawn()
        {
            while (!_spawnCancellation.IsCancellationRequested)
            {
                if (_enemySpawner.CanSpawn())
                {
                    var enemy = _enemySpawner.SpawnEnemy(GetEnemyInfo());
                    _activeEnemies.Add(enemy);
                    enemy.OnDestroy += Destroy;
                }

                await UniTask.WaitForSeconds(_spawnInterval);
            }
        }

        private void Destroy(Enemy enemy)
        {
            enemy.OnDestroy -= Destroy;
            _activeEnemies.Remove(enemy);
            _enemySpawner.ReturnEnemy(enemy);
        }

        private EnemyInfo GetEnemyInfo()
        {
            return new EnemyInfo
            {
                SpawnTransform = _enemyPositions.RandomSpawnPosition(),
                MoveTargetTransform = _enemyPositions.RandomAttackPosition(),
                AimTransform = _aimTransform
            };
        }

        public void OnGameStart()
        {
            StartSpawning();
        }

        public void OnGamePause()
        {
            StopSpawning();
        }

        public void OnGameResume()
        {
            StartSpawning();
        }

        public void OnGameFinish()
        {
            StopSpawning();

            for (int i = _activeEnemies.Count - 1; i >= 0; i--)
            {
                _activeEnemies[i].OnGameFinish();
                Destroy(_activeEnemies[i]);
            }

            _enemySpawner.Clear();
            _activeEnemies.Clear();
        }

        public void OnFixedUpdate(float deltaTime)
        {
            for (int i = 0; i < _activeEnemies.Count; i++)
            {
                _activeEnemies[i].OnFixedUpdate(deltaTime);
            }
        }
    }
}