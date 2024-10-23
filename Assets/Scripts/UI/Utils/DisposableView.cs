using UniRx;
using UnityEngine;

namespace UI.Utils
{
    public abstract class DisposableView : MonoBehaviour
    {
        protected CompositeDisposable _disposable = new();

        protected virtual void OnDisable()
        {
            _disposable?.Clear();
        }

        protected virtual void OnDestroy()
        {
            _disposable?.Clear();
        }

        protected void Dispose()
        {
            _disposable?.Clear();
            _disposable = new();
        }
    }
}