using Presenters.Interfaces.Common;
using UniRx;

namespace Presenters.Interfaces
{
    public interface IExperiencePresenter : IPresenter
    {
        IReadOnlyReactiveProperty<float> XP { get; }
        IReadOnlyReactiveProperty<string> XPInfo { get; }
    }
}