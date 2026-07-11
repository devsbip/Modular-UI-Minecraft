using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggleUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button _fullscreenButton;
    [SerializeField] private TextMeshProUGUI _fullscreenLabel;
    [SerializeField] private TextMeshProUGUI _fullscreenShadowLabel;

    [Header("Data")]
    [SerializeField] private LocalizationKey _localizationKey;
    [SerializeField] private LocalizationTableSO _localizationTable;
    [SerializeField] private GameSettingsSO _gameSettings;

    [Header("Internal State")]
    private bool _isFullscreen;

    private void Start()
    {
        _isFullscreen = _gameSettings.IsFullscreen;
        _gameSettings.OnLanguageChanged += OnLanguageChanged;

        ApplyFullscreenState();
        UpdateVisuals();

        _fullscreenButton.onClick.AddListener(ToggleFullscreen); 
    }

    private void OnDestroy()
    {
        _fullscreenButton.onClick.RemoveListener(ToggleFullscreen);
        _gameSettings.OnLanguageChanged -= OnLanguageChanged;
    }

    private void ToggleFullscreen()
    {
        _isFullscreen = !_isFullscreen;

        ApplyFullscreenState();
        _gameSettings.SetFullscreenMode(_isFullscreen);
        UpdateVisuals();
    }

    private void ApplyFullscreenState()
    {
        if (_isFullscreen)
        {
            Screen.fullScreenMode = _gameSettings.IsExclusiveFullscreen ? 
                FullScreenMode.ExclusiveFullScreen : 
                FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    private void OnLanguageChanged(GameLanguage newLanguage)
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (_localizationTable == null || _gameSettings == null || _localizationKey == LocalizationKey.None) return;

        GameLanguage currentLang = _gameSettings.CurrentLanguage;

        string translatedPrefix = _localizationTable.GetText(_localizationKey, currentLang);
        string statusText = "";

        if (currentLang == GameLanguage.English)
        {
            statusText = _isFullscreen ? "ON" : "OFF";
        }
        else if (currentLang == GameLanguage.Portuguese)
        {
            statusText = _isFullscreen ? "Sim" : "Não";
        }

        string displayText = $"{translatedPrefix}: {statusText}";

        if (_fullscreenLabel != null) _fullscreenLabel.text = displayText;
        if (_fullscreenShadowLabel != null) _fullscreenShadowLabel.text = displayText;
    }
}
