using Presenters.Interfaces;
using UI.Utils;
using UniRx;
using UnityEngine;

namespace UI.Views
{
    public class ExperienceView : DisposableView
    {
        [SerializeField] private ProgressBar _progressBar;

        public void Initialize(IExperiencePresenter presenter)
        {
            Dispose();
            SubscribeToPresenter(presenter);
        }

        private void SubscribeToPresenter(IExperiencePresenter presenter)
        {
            presenter.XP.Subscribe(_progressBar.UpdateProgressBar).AddTo(_disposable);
            presenter.XPInfo.Subscribe(_progressBar.UpdateProgressInfo).AddTo(_disposable);
        }
    }
}
