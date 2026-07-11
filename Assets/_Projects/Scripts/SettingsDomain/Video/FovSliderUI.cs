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
    [SerializeField] private LocalizationKey _localizationKey;
    [SerializeField] private LocalizationTableSO _localizationTable;
    [SerializeField] private GameSettingsSO _gameSettings;

    private void Start()
    {
        _fovSlider.onValueChanged.AddListener(ApplyFov);

        _gameSettings.OnLanguageChanged += OnLanguageChanged;

        _fovSlider.value = _gameSettings.FOV;
        UpdateVisuals(_fovSlider.value);
    }

    private void OnDestroy()
    {
        _fovSlider.onValueChanged.RemoveListener(ApplyFov);
        _gameSettings.OnLanguageChanged -= OnLanguageChanged;
    }

    private void OnLanguageChanged(GameLanguage newLanguage)
    {
        UpdateVisuals(_fovSlider.value);
    }

    private void ApplyFov(float currentFOV)
    {
        UpdateVisuals(currentFOV);
        _gameSettings.SetFOV(currentFOV);
    }

    private void UpdateVisuals(float currentFOV)
    {
        if (_localizationTable == null || _gameSettings == null || _localizationKey == LocalizationKey.None) return;

        GameLanguage currentLang = _gameSettings.CurrentLanguage;

        string translatedPrefix = _localizationTable.GetText(_localizationKey, currentLang);

        string statusText = "";

        if (currentFOV == 70f)
        {
            statusText = currentLang == GameLanguage.Portuguese ? "Padrão" : "Normal";
        }
        else if (currentFOV == 110f)
        {
            statusText = "FOV: Quake Pro";
        }
        else
        {
            statusText = currentFOV.ToString();
        }

        string displayText = $"{translatedPrefix}: {statusText}";

        if (_fovLabel != null) _fovLabel.text = displayText;
        if (_fovShadowLabel != null) _fovShadowLabel.text = displayText;
    }
}
