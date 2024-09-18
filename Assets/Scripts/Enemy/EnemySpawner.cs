using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private EnemyPool _pool;
        [SerializeField]
        private int _maxEnemyCount = 10;
   
        private int _enemiesCount;

        public void SpawnEnemy(EnemyInfo enemyInfo)
        {
            if (_enemiesCount >= _maxEnemyCount) return;
            var enemy = _pool.Pop(enemyInfo).Enemy;
            _enemiesCount++;
            enemy.OnEnemyDeath += ReturnEnemy;
        }

        public void ReturnEnemy(Enemy enemy)
        {
            enemy.OnEnemyDeath -= ReturnEnemy;
            _enemiesCount--;
            _pool.Push(enemy.PoolObject);
        }
    }
}