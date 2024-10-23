using Data;
using Presenters;
using Sirenix.OdinInspector;
using UI.Popup;
using UnityEngine;
using Zenject;

namespace Helpers
{
    public class CharacterPopupHelper : MonoBehaviour
    {
        private IPopup _characterPopup;
        private CharacterModel _characterModel;

        [Inject]
        private void Construct(CharacterModel characterModel, IPopup characterPopup)
        {
            _characterModel = characterModel;
            _characterPopup = characterPopup;
        }

        private void Awake()
        {
            HidePopup();
        }

        [Button]
        public void ShowPopup()
        {
            _characterPopup.Show(new CharacterPopupPresenter(_characterModel.UserInfo, _characterModel.CharacterInfo, _characterModel.PlayerLevel));
        }

        [Button]
        public void HidePopup()
        {
            _characterPopup.Hide();
        }
    }
}
