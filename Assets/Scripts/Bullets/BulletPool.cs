using Zenject;

namespace ShootEmUp
{
    public class BulletPool : MonoMemoryPool<BulletInfo, Bullet>
    {
        protected override void Reinitialize(BulletInfo bulletInfo, Bullet bullet)
        {
            bullet.Init(bulletInfo);
        }
    }
}