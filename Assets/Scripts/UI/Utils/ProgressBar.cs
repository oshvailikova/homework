using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Utils
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider _progressBar;
        [SerializeField] private Image _foreground;
        [SerializeField] private TMP_Text _progressInfo;

        [Header("Sprites")]
        [SerializeField] private Sprite _filledSprite;
        [SerializeField] private Sprite _notFilledSprite;

        public void UpdateProgressInfo(string info)
        {
            _progressInfo.text = info;
        }

        public void UpdateProgressBar(float value)
        {
            _progressBar.value = value;
            UpdateBarSprite();
        }

        private void UpdateBarSprite()
        {
            if (_progressBar.value < 1)
                _foreground.sprite = _notFilledSprite;
            else
                _foreground.sprite = _filledSprite;
        }
    }
}
