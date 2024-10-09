using ShootEmUp;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Installers
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private Bullet _bulletPrefab;
        [SerializeField]
        private Enemy _enemyPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
            Container.BindInterfacesTo<GameOverObserver>().AsSingle().NonLazy();

            BindLevel();

            BindPlayer();
            BindBulletSystem();
            BindEnemySystem();
            BindUI();
        }

        private void BindLevel()
        {
            Container.Bind<LevelBounds>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelBackground>().FromComponentInHierarchy().AsSingle();
        }

        private void BindPlayer()
        {
            Container.BindInterfacesAndSelfTo<Player>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputObserver>().AsSingle().NonLazy();
        }

        private void BindBulletSystem()
        {
            Container.BindInterfacesAndSelfTo<BulletSpawner>().AsSingle();

            Container.BindInterfacesAndSelfTo<BulletManager>().AsSingle().NonLazy();

            Container.BindMemoryPool<Bullet, BulletPool>()
                .FromComponentInNewPrefab(_bulletPrefab)
                .UnderTransformGroup("BulletSystem");
        }

        private void BindEnemySystem()
        {
            Container.Bind<EnemyPositions>().FromComponentInHierarchy().AsSingle();
            
            Container.BindInterfacesAndSelfTo<EnemyManager>().AsSingle().NonLazy();

            Container.BindMemoryPool<Enemy, EnemyPool>()
            .FromComponentInNewPrefab(_enemyPrefab)
            .UnderTransformGroup("EnemySystem");
        }

        private void BindUI()
        {
            Container.BindInterfacesTo<UIGameSystemController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CountdownDisplay>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CountdownLauncher>().AsSingle();
        }
    }
}