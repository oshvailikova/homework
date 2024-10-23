using Data;
using Presenters.Interfaces;
using UniRx;
using CharacterInfo = Data.CharacterInfo;

namespace Presenters
{
    public class CharacterPopupPresenter : ICharacterPopupPresenter
    {
        public IUserInfoPresenter InfoPresenter { get => _infoPresenter; }
        public IExperiencePresenter ExperiencePresenter { get => _experiencePresenter; }
        public IStatsContainerPresenter StatsContainerPresenter { get => _statsContainerPresenter; }


        public IReadOnlyReactiveProperty<bool> CanLevelUp { get => _canLevelUp; }
        public ReactiveCommand LevelUpCommand { get => _levelUpCommand; }


        private readonly PlayerLevel _playerLevel;

        private readonly IUserInfoPresenter _infoPresenter;
        private readonly IExperiencePresenter _experiencePresenter;
        private readonly IStatsContainerPresenter _statsContainerPresenter;

        private readonly BoolReactiveProperty _canLevelUp;
        private readonly ReactiveCommand _levelUpCommand;

        private readonly CompositeDisposable subscriptions = new();

        public CharacterPopupPresenter(UserInfo userInfo, CharacterInfo characterInfo, PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;

            _infoPresenter = new UserInfoPresenter(userInfo, playerLevel);
            _experiencePresenter = new CharacterExperiencePresenter(playerLevel);
            _statsContainerPresenter = new CharacterStatsContainerPresenter(characterInfo);

            _canLevelUp = new BoolReactiveProperty(playerLevel.CanLevelUp());
            _levelUpCommand = new ReactiveCommand(_canLevelUp);

            _playerLevel.OnExperienceChanged += OnChangedExperience;

            _levelUpCommand.Subscribe(OnLevelUpCommand).AddTo(subscriptions);
        }

        ~CharacterPopupPresenter()
        {
            _playerLevel.OnExperienceChanged -= OnChangedExperience;
            subscriptions.Dispose();
        }

        private void OnLevelUpCommand(Unit _)
        {
            _playerLevel.LevelUp();
        }
        private void OnChangedExperience(int value)
        {
            _canLevelUp.Value = _playerLevel.CanLevelUp();
        }
    }
}
