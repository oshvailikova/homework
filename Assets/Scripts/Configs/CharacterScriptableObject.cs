using System;
using System.Collections.Generic;
using Data;
using Data.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Configs
{
    [CreateAssetMenu(fileName = "CharacterScriptableObject", menuName = "ScriptableObjects/Character", order = 1)]
    public class CharacterScriptableObject : ScriptableObject
    {
        public string Nickname;
        public string Description;
        public Sprite Icon;
        public int Level;
        public int Experience;
        [SerializeField]
        public List<CharacterStat> CharacterStats;

        [Button]
        public void AddRandomStats()
        {
            CharacterStats = new();
            var statTypes = Enum.GetValues(typeof(StatType));
            foreach (var statType in statTypes)
            {
                var stat = new CharacterStat(statType.ToString(), Random.Range(0, 100));
                CharacterStats.Add(stat);
            }
        }
    }
}
