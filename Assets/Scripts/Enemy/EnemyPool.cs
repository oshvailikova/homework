using ShootEmUp;
using Zenject;

public class EnemyPool : MonoMemoryPool<EnemyInfo, Enemy>
{
    protected override void Reinitialize(EnemyInfo enemyInfo, Enemy enemy)
    {
        enemy.Init(enemyInfo);
    }
}