using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    public sealed class ButtonController : MonoBehaviour, IButtonController
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void Show()
        {
            _button.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _button.gameObject.SetActive(false);
        }

        public void AddClickListener(UnityEngine.Events.UnityAction listener)
        {
            _button.onClick.AddListener(listener);
        }

        public void RemoveListeners()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}