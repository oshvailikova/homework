using Data;
using Data.Utils;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Helpers
{
    public class CharacterModelHelper : MonoBehaviour
    {
        [ShowInInspector] private CharacterModel _characterModel;

        [Inject]
        public void Construct(CharacterModel characterInfoPopupModel)
        {
            _characterModel = characterInfoPopupModel;
        }

        [Button]
        public void AddStat(StatType statType, int value)
        {
            _characterModel.AddCharacterStat(statType.ToString(), value);
        }

        [Button]
        public void RemoveStat(StatType statType)
        {
             _characterModel.TryRemoveCharacterStat(statType.ToString());
        }

        [Button]
        public void AddRandomStats()
        {
            var statTypes = Enum.GetValues(typeof(StatType));
            foreach (var statType in statTypes)
            {
                _characterModel.AddCharacterStat(statType.ToString(), Random.Range(0, 100));
            }
        }
    }
}