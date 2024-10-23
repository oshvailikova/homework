using UniRx;

namespace Presenters.Interfaces
{
    public interface IStatPropertyPresenter
    {
       IReadOnlyReactiveProperty<string> StatInfo { get; }
    }
}