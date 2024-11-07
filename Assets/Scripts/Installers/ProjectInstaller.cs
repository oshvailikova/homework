using Zenject;
using SaveSystem;
using SaveSystem.Utils;
using SaveSystem.SnapshotSystem;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameRepository>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<FileSaveLoadSystem>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameStateManager>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<JsonFileStorage>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AesEncryptor>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<JsonSerializationService>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SnapshotManager>().FromNew().AsSingle().NonLazy();
        }
    }
}