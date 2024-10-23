using Data;
using Presenters.Interfaces;
using UniRx;
using UnityEngine;


namespace Presenters
{
    public class UserInfoPresenter : IUserInfoPresenter
    {
        public IReadOnlyReactiveProperty<string> Name { get => _name; }
        public IReadOnlyReactiveProperty<string> Description { get => _description; }
        public IReadOnlyReactiveProperty<string> Level { get => _level; }
        public IReadOnlyReactiveProperty<Sprite> Icon { get => _icon; }


        private readonly UserInfo _userInfo;
        private readonly PlayerLevel _playerLevel;

        private readonly StringReactiveProperty _name;
        private readonly StringReactiveProperty _description;
        private readonly StringReactiveProperty _level;
        private readonly ReactiveProperty<Sprite> _icon;
        public UserInfoPresenter(UserInfo userInfo, PlayerLevel playerLevel)
        {
            _userInfo = userInfo;
            _playerLevel = playerLevel;

            _name = new StringReactiveProperty(_userInfo.Name);
            _description = new StringReactiveProperty(_userInfo.Description);
            _icon = new ReactiveProperty<Sprite>(_userInfo.Icon);

            _level = new StringReactiveProperty(GetLevelString());

            _userInfo.OnNameChanged += OnNameChanged;
            _userInfo.OnDescriptionChanged += OnDescriptionChanged;
            _userInfo.OnIconChanged += OnIconChanged;

            _playerLevel.OnLevelUp += OnChangedLevel;
        }

        ~UserInfoPresenter()
        {
            _userInfo.OnNameChanged -= OnNameChanged;
            _userInfo.OnDescriptionChanged -= OnDescriptionChanged;
            _userInfo.OnIconChanged -= OnIconChanged;

            _playerLevel.OnLevelUp -= OnChangedLevel;
        }

        private void OnNameChanged(string name)
        {
            _name.Value = name;
        }

        private void OnDescriptionChanged(string description)
        {
            _description.Value = description;
        }

        private void OnIconChanged(Sprite icon)
        {
            _icon.Value = icon;
        }

        private void OnChangedLevel()
        {
            _level.Value = GetLevelString();
        }

        private string GetLevelString()
        {
            return $"Level: {_playerLevel.CurrentLevel.ToString()}";
        }
    }
}