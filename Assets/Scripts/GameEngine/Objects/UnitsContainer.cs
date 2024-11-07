using System.Collections.Generic;
using UnityEngine;

namespace GameEngine
{
    [CreateAssetMenu(fileName = "UnitsContainer", menuName = "GameEngine/UnitsContainer")]
    public class UnitsContainer : ScriptableObject
    {
        [SerializeField]
        private List<Unit> unitPrefabEntries;

        public Unit GetPrefabByType(string type)
        {
            var entry = unitPrefabEntries.Find(e => e.Type == type);
            return entry;
        }
    }
}