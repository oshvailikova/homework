using UnityEngine;
using Common;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "CountdownLauncher",
        menuName = "UI/New CountdownLauncherConfig"
    )]
    public sealed class CountdownLauncherConfig : ScriptableObject
    {
        [SerializeField]
        private int _countdownValue;
        public int CountdownValue
        {
            get => _countdownValue;
        }
    }
}