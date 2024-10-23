using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Utils
{
    public class SpriteSwitchButton : MonoBehaviour
    {
        [SerializeField]
        private Sprite _activeSprite;
        [SerializeField]
        private Sprite _inactiveSprite;

        private Image _buttonImage;
        private Button _button;

        public Button Button
        {
            get => _button;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _buttonImage = GetComponent<Image>();

            UpdateSprite(_button.interactable);
        }

        public void AddListener(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        public void RemoveListener(UnityAction action)
        {
            _button.onClick.RemoveListener(action);
        }

        public void SetActive(bool isActive)
        {
            _button.interactable = isActive;
            UpdateSprite(isActive);
        }

        private void UpdateSprite(bool interactable)
        {
            _buttonImage.sprite = interactable ? _activeSprite : _inactiveSprite;
        }
    }
}