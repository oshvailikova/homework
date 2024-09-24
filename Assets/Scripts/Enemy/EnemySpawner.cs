using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private ObjectPool _pool;
        [SerializeField]
        private int _maxEnemyCount = 10;
   
        private int _enemiesCount;

        public void Clear()
        {
            _enemiesCount = 0;
        }

        public void SpawnEnemy(EnemyInfo enemyInfo)
        {
            if (_enemiesCount >= _maxEnemyCount) return;
            var enemy = _pool.GetFromPool<Enemy>();
            enemy.Init(enemyInfo);
            _enemiesCount++;
            enemy.OnDestroy += ReturnEnemy;
        }

        private void ReturnEnemy(Enemy enemy)
        {
            enemy.OnDestroy -= ReturnEnemy;
            _enemiesCount--;
            _pool.ReturnToPool(enemy.gameObject);
        }


    }

}