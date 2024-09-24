using UnityEngine;
using TMPro;


namespace ShootEmUp
{
    public class CountdownDisplay : MonoBehaviour
    {
        private TextMeshProUGUI _countdownText;

        private void Awake()
        {
            _countdownText = GetComponent<TextMeshProUGUI>();
            SetVisibility(false);
        }

        public void SetVisibility(bool isVisible)
        {
            if (!isVisible)
                Clear();
            _countdownText.gameObject.SetActive(isVisible);
        }

        public void UpdateDisplay(float remainingTime)
        {            
            _countdownText.text = Mathf.CeilToInt(remainingTime).ToString();
        }

        private void Clear()
        {
            _countdownText.text = "";
        }
    }
}