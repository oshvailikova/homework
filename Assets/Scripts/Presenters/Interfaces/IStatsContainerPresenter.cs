using Presenters.Interfaces.Common;
using UI.Interfaces.Presenters;
using UniRx;

namespace Presenters.Interfaces
{
    public interface IStatsContainerPresenter : IPresenter
    {
        IReadOnlyReactiveCollection<IStatPropertyPresenter> StatPropertyPresenters { get; }
    }
}