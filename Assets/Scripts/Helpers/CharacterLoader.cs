using Configs;
using Data;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Helpers
{
    public class CharacterLoader : MonoBehaviour
    {
        [SerializeField] private CharacterScriptableObject _characterScriptableObject;

        private CharacterModel _characterInfoModel;

        [Inject]
        public void Construct(CharacterModel characterInfoModel)
        {
            _characterInfoModel = characterInfoModel;
            LoadCharacter();
        }

        [Button]
        public void LoadCharacter()
        {
            if (_characterInfoModel == null)
                return;

            _characterInfoModel.UpdateUserInfo(
                _characterScriptableObject.Nickname,
                _characterScriptableObject.Description,
                _characterScriptableObject.Icon);

            foreach (var characterStat in _characterScriptableObject.CharacterStats)
            {
                _characterInfoModel.AddCharacterStat(characterStat);
            }

            _characterInfoModel.UpdatePlayerLevel(_characterScriptableObject.Level, _characterScriptableObject.Experience);
        }
    }
}