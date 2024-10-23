using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Zenject;

namespace Data
{
    [Serializable]
    public sealed class CharacterModel
    {
        [ShowInInspector]
        private CharacterInfo _characterInfo;
        [ShowInInspector]
        private UserInfo _userInfo;
        [ShowInInspector]
        private PlayerLevel _playerLevel;

        public CharacterInfo CharacterInfo => _characterInfo;
        public UserInfo UserInfo => _userInfo;
        public PlayerLevel PlayerLevel => _playerLevel;

        [Inject]
        public void Construct(UserInfo userInfo, CharacterInfo characterInfo, PlayerLevel playerLevel)
        {
            _userInfo = userInfo;
            _characterInfo = characterInfo;
            _playerLevel = playerLevel;
        }

        public void UpdateUserInfo(string nickname, string description, Sprite icon)
        {
            _userInfo.ChangeName(nickname);
            _userInfo.ChangeDescription(description);
            _userInfo.ChangeIcon(icon);
        }

        public void UpdatePlayerLevel(int level, int experience)
        {
            _playerLevel.SetLevel(level);
            _playerLevel.SetExperience(experience);
        }

        public CharacterStat GetCharacterStat(string name)
        {
            return _characterInfo.GetStat(name);
        }

        public void AddCharacterStat(string name, int value)
        {
            try
            {
                var stat = _characterInfo.GetStat(name);
                stat.ChangeValue(value);
            }
            catch
            {
                var stat = new CharacterStat(name, value);
                _characterInfo.AddStat(stat);
            }
        }

        public void AddCharacterStat(CharacterStat characterStat)
        {
            if (_characterInfo.GetStats().Length > 0)
                TryRemoveCharacterStat(characterStat.Name);
            _characterInfo.AddStat(characterStat);
        }

        public void TryRemoveCharacterStat(string name)
        {
            try
            {
                var stat = _characterInfo.GetStat(name);
                _characterInfo.RemoveStat(stat);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }
    }
}