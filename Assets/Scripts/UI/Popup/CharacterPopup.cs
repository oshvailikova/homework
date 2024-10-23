using Presenters.Interfaces;
using Presenters.Interfaces.Common;
using System;
using UI.Utils;
using UI.Views;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class CharacterPopup : DisposableView, IPopup
    {
        [Header("Views")]
        [SerializeField] private UserInfoView _infoView;
        [SerializeField] private ExperienceView _experienceView;
        [SerializeField] private StatsContainer _statsContainer;

        [Header("Buttons")]
        [SerializeField] private Button _closeButton;
        [SerializeField] private SpriteSwitchButton _levelUpButton;

        private ICharacterPopupPresenter _currentPresenter;

        public void Show(IPresenter presenter)
        {
            if (presenter is not ICharacterPopupPresenter characterPresenter)
            {
                throw new InvalidOperationException("Expected CharacterPresenter");
            }

            _currentPresenter = characterPresenter;

            gameObject.SetActive(true);

            SubscribeToPresenter(_currentPresenter);

            _infoView.Initialize(_currentPresenter.InfoPresenter);
            _experienceView.Initialize(_currentPresenter.ExperiencePresenter);
            _statsContainer.Initialize(_currentPresenter.StatsContainerPresenter);

            _closeButton.onClick.AddListener(Hide);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _closeButton.onClick.RemoveListener(Hide);
        }

        private void SubscribeToPresenter(ICharacterPopupPresenter presenter)
        {
            presenter.CanLevelUp.Subscribe(OnLevelUpConditionChanged).AddTo(_disposable);
            presenter.LevelUpCommand.BindTo(_levelUpButton.Button).AddTo(_disposable);
        }

        private void OnLevelUpConditionChanged(bool canLevelUp)
        {
            _levelUpButton.SetActive(canLevelUp);
        }
    }
}