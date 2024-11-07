using DataHandlers;
using GameEngine;
using SaveSystem;
using Zenject;

namespace Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Unit>().FromComponentsInHierarchy().AsCached();
            Container.BindInterfacesAndSelfTo<UnitManager>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<UnitsDataHandler>().FromNew().AsSingle();

            Container.Bind<Resource>().FromComponentsInHierarchy().AsCached();
            Container.BindInterfacesAndSelfTo<ResourceService>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<ResourcesDataHandler>().FromNew().AsSingle();

        }
    }
}