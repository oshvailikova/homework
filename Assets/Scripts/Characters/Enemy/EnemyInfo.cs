using UnityEngine;

namespace ShootEmUp
{
    public struct EnemyInfo
    {
        public Transform SpawnTransform;
        public Transform MoveTargetTransform;
        public Transform AimTransform;
        public IBulletSpawner BulletSpawner;
        public LevelBounds LevelBounds;
    }
}