using TMPro;
using UI.Interfaces.Presenters;
using UI.Utils;
using UniRx;
using UnityEngine;

namespace UI.Views
{
    public class StatProperty : DisposableView
    {
        [SerializeField] private TMP_Text _description;

        private IStatPropertyPresenter _currentPresenter;

        public void Initialize(IStatPropertyPresenter presenter)
        {
            Dispose();
            _currentPresenter = presenter;
            SubscribeToPresenter(_currentPresenter);
        }

        private void SubscribeToPresenter(IStatPropertyPresenter presenter)
        {
            presenter.StatInfo.Subscribe(OnLevelChanged).AddTo(_disposable);
        }

        private void OnLevelChanged(string newValue)
        {
            _description.text = newValue;
        }
    }
}
