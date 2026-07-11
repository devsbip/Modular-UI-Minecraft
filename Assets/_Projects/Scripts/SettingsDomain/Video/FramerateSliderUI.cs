using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FramerateSliderUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider _framerateSlider;
    [SerializeField] private TextMeshProUGUI _framerateLabel;
    [SerializeField] private TextMeshProUGUI _framerateShadowLabel;

    [Header("Data")]
    [SerializeField] private LocalizationKey _localizationKey;
    [SerializeField] private LocalizationTableSO _localizationTable;
    [SerializeField] private GameSettingsSO _gameSettings;

    private int _pendingFramerate;

    private void Start()
    {
        _framerateSlider.value = _gameSettings.MaxFramerate;
        CalculateFramerateTarget(_framerateSlider.value);

        _framerateSlider.onValueChanged.AddListener(CalculateFramerateTarget);
        _gameSettings.OnLanguageChanged += OnLanguageChanged;
    }

    private void OnDestroy()
    {
        _framerateSlider.onValueChanged.RemoveListener(CalculateFramerateTarget);
        _gameSettings.OnLanguageChanged -= OnLanguageChanged;
    }

    private void CalculateFramerateTarget(float sliderIndex)
    {
        if (sliderIndex == 22)
        {
            _pendingFramerate = -1;
        }
        else
        {
            _pendingFramerate = 30 + ((int)sliderIndex * 10);
        }

        UpdateVisuals(sliderIndex);
    }

    private void OnLanguageChanged(GameLanguage newLanguage)
    {
        UpdateVisuals(_framerateSlider.value);
    }

    public void ConfirmFramerateSettings()
    {
        Application.targetFrameRate = _pendingFramerate;
        _gameSettings.SetFramerate((int)_framerateSlider.value);
    }

    private void UpdateVisuals(float sliderIndex)
    {
        if (_localizationTable == null || _gameSettings == null || _localizationKey == LocalizationKey.None) return;

        GameLanguage currentLang = _gameSettings.CurrentLanguage;

        string translatedPrefix = _localizationTable.GetText(_localizationKey, currentLang);
        string statusText;

        if (sliderIndex == 22)
        {
            statusText = currentLang == GameLanguage.Portuguese ? "Ilimitado" : "Unlimited";
        }else
        {
            statusText = $"{_pendingFramerate} fps";
        }

        string displayText = $"{translatedPrefix}: {statusText}";

        if (_framerateLabel != null) _framerateLabel.text = displayText;
        if (_framerateShadowLabel != null) _framerateShadowLabel.text = displayText;
    }
}
