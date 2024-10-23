using Presenters.Interfaces;
using TMPro;
using UI.Utils;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class UserInfoView : DisposableView
    {
        [SerializeField] private TMP_Text _level;

        [SerializeField] private TMP_Text _characterName;
        [SerializeField] private Image _avatar;
        [SerializeField] private TMP_Text _description;

        private IUserInfoPresenter _currentPresenter;

        public void Initialize(IUserInfoPresenter presenter)
        {
            Dispose();

            _currentPresenter = presenter;
            SubscribeToPresenter(_currentPresenter);
        }

        private void SubscribeToPresenter(IUserInfoPresenter presenter)
        {
            presenter.Name.Subscribe(OnNameChanged).AddTo(_disposable);
            presenter.Description.Subscribe(OnDescriptionChanged).AddTo(_disposable);
            presenter.Icon.Subscribe(OnIconChanged).AddTo(_disposable);
            presenter.Level.Subscribe(UpdateLevelValue).AddTo(_disposable);
        }

        private void OnNameChanged(string value)
        {
            _characterName.text = value;
        }

        private void OnDescriptionChanged(string value)
        {
            _description.text = value;
        }

        private void OnIconChanged(Sprite icon)
        {
            _avatar.sprite = icon;
        }

        private void UpdateLevelValue(string level)
        {
            _level.text = level;
        }
    }
}

