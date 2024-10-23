using Data;
using Helpers;
using UI.Popup;
using Zenject;

namespace PresentationModel
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CharacterInfo>().AsCached().NonLazy();
            Container.Bind<UserInfo>().AsCached().NonLazy();
            Container.Bind<PlayerLevel>().AsCached().NonLazy();

            Container.Bind<CharacterModel>().AsCached().NonLazy();

            Container.BindInterfacesAndSelfTo<CharacterPopup>().FromComponentInHierarchy().AsSingle();
           // Container.Bind<CharacterPopupHelper>()
        }
    }
}
