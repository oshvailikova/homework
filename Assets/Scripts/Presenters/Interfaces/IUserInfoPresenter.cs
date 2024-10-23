using Presenters.Interfaces.Common;
using UniRx;
using UnityEngine;

namespace Presenters.Interfaces
{
    public interface IUserInfoPresenter : IPresenter
    {
        IReadOnlyReactiveProperty<string> Name { get; }
        IReadOnlyReactiveProperty<string> Description { get; }
        IReadOnlyReactiveProperty<string> Level { get; }
        IReadOnlyReactiveProperty<Sprite> Icon { get; }
    }
}
