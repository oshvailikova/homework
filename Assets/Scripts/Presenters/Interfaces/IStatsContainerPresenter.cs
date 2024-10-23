using Presenters.Interfaces.Common;
using UniRx;

namespace Presenters.Interfaces
{
    public interface IStatsContainerPresenter : IPresenter
    {
        IReadOnlyReactiveCollection<IStatPropertyPresenter> StatPropertyPresenters { get; }
    }
}