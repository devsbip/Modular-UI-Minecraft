using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FOVSliderUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider _fovSlider;
    [SerializeField] private TextMeshProUGUI _fovLabel;
    [SerializeField] private TextMeshProUGUI _fovShadowLabel;

    [Header("Data")]
    [SerializeField] private GameSettingsSO _gameSettings;

    private void Start()
    {
        _fovSlider.onValueChanged.AddListener(ApplyFov);

        _fovSlider.value = _gameSettings.FOV;
        ApplyFov(_fovSlider.value);
    }

    private void OnDestroy()
    {
        _fovSlider.onValueChanged.RemoveListener(ApplyFov);
    }

    private void ApplyFov(float currentFOV)
    {
        

        _gameSettings.SetFOV(currentFOV);
    }

    private void UpdateVisuals(float currentFOV)
    {
        string displayText;

        if (currentFOV == 70f)
        {
            displayText = "FOV:Normal";
        }
        else if (currentFOV == 110f)
        {
            displayText = "FOV: Quake Pro";
        }
        else
        {
            displayText = $"FOV: {currentFOV}";
        }

        if (_fovLabel != null) _fovLabel.text = displayText;
        if (_fovShadowLabel != null) _fovShadowLabel.text = displayText;
    }
}
