using UniRx;

namespace UI.Interfaces.Presenters
{
    public interface IStatPropertyPresenter
    {
       IReadOnlyReactiveProperty<string> StatInfo { get; }
    }
}