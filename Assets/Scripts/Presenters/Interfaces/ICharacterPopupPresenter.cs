using Presenters.Interfaces.Common;
using UniRx;

namespace Presenters.Interfaces
{
    public interface ICharacterPopupPresenter : IPresenter
    {
        IUserInfoPresenter InfoPresenter { get; }
        IExperiencePresenter ExperiencePresenter { get; }
        IStatsContainerPresenter StatsContainerPresenter { get; }


        IReadOnlyReactiveProperty<bool> CanLevelUp { get; }
        ReactiveCommand LevelUpCommand { get; }
    }
}