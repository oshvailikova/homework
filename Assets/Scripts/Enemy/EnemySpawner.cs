namespace ShootEmUp
{
    public sealed class EnemySpawner 
    {      
   
        private EnemyPool _enemyPool;

        private int _maxEnemyCount;
        private int _enemiesCount;

        public EnemySpawner(int maxEnemyCount, EnemyPool enemyPool)
        {
            _maxEnemyCount = maxEnemyCount;
            _enemyPool = enemyPool;
        }

        public void Clear()
        {
            _enemiesCount = 0;
        }

        public bool CanSpawn()
        {
            return _enemiesCount < _maxEnemyCount;
        }

        public Enemy SpawnEnemy(EnemyInfo enemyInfo)
        {
            _enemiesCount++;
            return _enemyPool.Spawn(enemyInfo);
        }

        public void ReturnEnemy(Enemy enemy)
        {
            _enemiesCount--;
            _enemyPool.Despawn(enemy);
        }


    }

}