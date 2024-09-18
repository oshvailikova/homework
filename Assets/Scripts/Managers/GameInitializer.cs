using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameInitializer : MonoBehaviour
    {
        [SerializeField]
        private LevelBounds _levelBounds;
        [SerializeField]
        private ShootEventManager _shootEventManager;

        private InitilizableObject[] initilizableObjects;

        private void Awake()
        {
            initilizableObjects = GameObject.FindObjectsOfType<InitilizableObject>();
        }

        private void Start()
        {
            foreach (var initializable in initilizableObjects)
            {
                initializable.Initialize(_levelBounds, _shootEventManager);
            }
        }
    }
}