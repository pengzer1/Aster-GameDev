using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AG.UI
{
    public class SliderCountDisplay : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI displayText;
        [SerializeField]
        private Slider sliderToDisplay;

        private void Start()
        {
            sliderToDisplay.onValueChanged.AddListener((float value) =>
                displayText.text = ((int)value).ToString()
            );
        }
    }
}