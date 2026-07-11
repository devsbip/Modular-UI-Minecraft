using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider _audioSlider;
    [SerializeField] private TextMeshProUGUI _audioLabel;
    [SerializeField] private TextMeshProUGUI _audioShadowLabel;

    [Header("Data")]
    [SerializeField] private LocalizationKey _localizationKey; 
    [SerializeField] private LocalizationTableSO _localizationTable;
    [SerializeField] private GameSettingsSO _gameSettings;

    [Header("Settings")]
    [SerializeField] private AudioChannel _myChannel;

    private void Start()
    {
        float savedVolume = _gameSettings.GetVolume(_myChannel);
        _audioSlider.value = savedVolume;
        _gameSettings.OnLanguageChanged += OnLanguageChanged;

        UpdateVisuals(savedVolume);

        _audioSlider.onValueChanged.AddListener(ApplyVolume);
    }

    private void OnDestroy()
    {
        _audioSlider.onValueChanged.RemoveListener(ApplyVolume);
        _gameSettings.OnLanguageChanged -= OnLanguageChanged;
    }

    private void ApplyVolume(float currentValue)
    {
        UpdateVisuals(currentValue);
        _gameSettings.SetVolume(_myChannel, currentValue);
    }

    private void OnLanguageChanged(GameLanguage newLanguage)
    {
        UpdateVisuals(_audioSlider.value);
    }

    private void UpdateVisuals(float currentValue)
    {
        if (_localizationTable == null || _gameSettings == null || _localizationKey == LocalizationKey.None) return;

        GameLanguage currentLang = _gameSettings.CurrentLanguage;

        string translatedPrefix = _localizationTable.GetText(_localizationKey, currentLang);
        int displayPercentage = Mathf.RoundToInt(currentValue * 100);
        string statusText;

        if (displayPercentage == 0)
        {
            statusText = currentLang == GameLanguage.Portuguese ? "Não" : "OFF";
        }
        else
        {
            statusText = $"{displayPercentage}%";
        }

        string displayText = $"{translatedPrefix}: {statusText}";

        if (_audioLabel != null) _audioLabel.text = displayText;
        if (_audioShadowLabel != null) _audioShadowLabel.text = displayText;
    }
}
