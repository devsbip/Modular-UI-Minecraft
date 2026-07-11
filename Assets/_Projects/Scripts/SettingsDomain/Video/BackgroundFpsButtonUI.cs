using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundFpsButtonUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button _fpsButton;
    [SerializeField] private TextMeshProUGUI _fpsLabel;
    [SerializeField] private TextMeshProUGUI _fpsShadowLabel;

    [Header("Data")]
    [SerializeField] private LocalizationKey _localizationKey;
    [SerializeField] private LocalizationTableSO _localizationTable;
    [SerializeField] private GameSettingsSO _gameSettings;

    private BackgroundFpsMode _currentMode;

    private void Start()
    {
        _currentMode = _gameSettings.BackgroundMode;
        _gameSettings.OnLanguageChanged += OnLanguageChanged;

        UpdateVisuals();
        _fpsButton.onClick.AddListener(CycleBackgroundMode);
    }

    private void OnDestroy()
    {
        _fpsButton.onClick.RemoveListener(CycleBackgroundMode);
        _gameSettings.OnLanguageChanged -= OnLanguageChanged;
    }

    private void CycleBackgroundMode()
    {
        _currentMode = _currentMode == BackgroundFpsMode.Minimized ? BackgroundFpsMode.Afk : BackgroundFpsMode.Minimized;

        _gameSettings.SetBackgroundFpsMode(_currentMode);
        UpdateVisuals();
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
            statusText = _currentMode == BackgroundFpsMode.Minimized ? "Minimized" : "AFK";
        }
        else if (currentLang == GameLanguage.Portuguese)
        {
            statusText = _currentMode == BackgroundFpsMode.Minimized ? "Minimizar" : "Ausentar-se";
        }

        string displayText = $"{translatedPrefix}: {statusText}";

        if (_fpsLabel != null) _fpsLabel.text = displayText;
        if (_fpsShadowLabel != null) _fpsShadowLabel.text = displayText;
    }
}
