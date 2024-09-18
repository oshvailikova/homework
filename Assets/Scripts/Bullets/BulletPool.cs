using Common;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletPool : ObjectPool<BulletPool, BulletPoolObject, BulletInfo> { }

    public class BulletPoolObject : PoolObject<BulletPool, BulletPoolObject, BulletInfo>
    {
        public Bullet Bullet;

        protected override void SetReferences()
        {
            Bullet = instance.GetComponent<Bullet>();
            Bullet.SetPoolObject(this);
        }

        public override void WakeUp(BulletInfo info)
        {
            base.WakeUp();
            Bullet.Init(info);
        }

    }

    public struct BulletInfo
    {
        public Vector2 Position;
        public bool Player;
        public Vector2 Direction;
        public BulletConfig BulletConfig;
        public LevelBounds LevelBounds;
    }
}