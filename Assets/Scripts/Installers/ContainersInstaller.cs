using GameEngine;
using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(
        fileName = "ContainersInstaller",
        menuName = "Installers/ContainersInstaller")]
    public sealed class ContainersInstaller : ScriptableObjectInstaller<ContainersInstaller>
    {
        [SerializeField]
        private UnitsContainer _unitsContainer;


        public override void InstallBindings()
        {
            Container.Bind<UnitsContainer>().FromInstance(_unitsContainer).AsSingle();
        }
    }
}