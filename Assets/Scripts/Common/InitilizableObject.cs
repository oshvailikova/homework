using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShootEmUp
{
    public class InitilizableObject : MonoBehaviour, IInitializable
    {
        protected ShootEventManager _shootEventManager;
        protected LevelBounds _levelBounds;

        public virtual void Initialize(LevelBounds levelBounds, ShootEventManager shootEventManager)
        {
            _levelBounds = levelBounds;
            _shootEventManager = shootEventManager;
        }
    }
}
