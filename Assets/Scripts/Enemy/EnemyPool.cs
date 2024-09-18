using Common;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyPool : ObjectPool<EnemyPool, EnemyPoolObject, EnemyInfo> { }

    public class EnemyPoolObject : PoolObject<EnemyPool, EnemyPoolObject,EnemyInfo>
    {
        public Enemy Enemy;

        protected override void SetReferences()
        {
            Enemy = instance.GetComponent<Enemy>();
            Enemy.SetPoolObject(this);
        }

        public override void WakeUp(EnemyInfo info)
        {
            base.WakeUp();
            Enemy.Init(info);
        }

       /* public override void Sleep()
        {
            instance.transform.parent = objectPool.transform;
            instance.SetActive(false);
        }*/

    }

    public struct EnemyInfo
    {
        public Transform SpawnTransform;
        public Transform MoveTargetTransform;
        public Transform AimTransform;
        public ShootEventManager ShootEventManager;
        public LevelBounds LevelBounds;
    }
}