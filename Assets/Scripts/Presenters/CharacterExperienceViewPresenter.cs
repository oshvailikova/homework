using Data;
using Presenters.Interfaces;
using UniRx;

namespace Presenters
{
    public class CharacterExperiencePresenter : IExperiencePresenter
    {
        public IReadOnlyReactiveProperty<float> XP { get => _xp; }
        public IReadOnlyReactiveProperty<string> XPInfo { get => _xpInfo; }

        
        private readonly PlayerLevel _playerLevel;

        private readonly FloatReactiveProperty _xp;
        private readonly StringReactiveProperty _xpInfo;

        public CharacterExperiencePresenter(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;

            _xp = new FloatReactiveProperty(GetXPValue());
            _xpInfo = new StringReactiveProperty(GetXPInfo());

            _playerLevel.OnExperienceChanged += OnChangedExperience;
        }

        ~CharacterExperiencePresenter()
        {
            _playerLevel.OnExperienceChanged -= OnChangedExperience;
        }

        private void OnChangedExperience(int _)
        {
            _xp.Value = GetXPValue();
            _xpInfo.Value = GetXPInfo();
        }

        private float GetXPValue()
        {
            return _playerLevel.CurrentExperience / (float)_playerLevel.RequiredExperience;
        }

        private string GetXPInfo()
        {
            return $"{_playerLevel.CurrentExperience}/{_playerLevel.RequiredExperience} XP";
        }
    }
}
