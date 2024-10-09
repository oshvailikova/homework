using UnityEngine;
using Zenject;

namespace ShootEmUp.Installers
{
    [CreateAssetMenu(
        fileName = "ConfigsInstaller",
        menuName = "Installers/ConfigsInstaller")]
    public sealed class ConfigsInstaller : ScriptableObjectInstaller<ConfigsInstaller>
    {
        [SerializeField]
        private PlayerConfig _playerConfig;

        [SerializeField]
        private EnemySpawnConfig _enemySpawnConfig;
        [SerializeField]
        private CountdownLauncherConfig _countdownLauncherConfig;

        public override void InstallBindings()
        {
            Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();

            Container.Bind<EnemySpawnConfig>().FromInstance(_enemySpawnConfig).AsSingle();

            Container.Bind<CountdownLauncherConfig>().FromInstance(_countdownLauncherConfig).AsSingle(); ;
        }
    }
}