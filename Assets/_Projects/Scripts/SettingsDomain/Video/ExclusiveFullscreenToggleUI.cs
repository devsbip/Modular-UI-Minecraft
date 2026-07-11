using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExclusiveFullscreenToggleUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button _exclusiveButton;
    [SerializeField] private TextMeshProUGUI _exclusiveLabel;
    [SerializeField] private TextMeshProUGUI _exclusiveShadowLabel;

    [Header("Data")]
    [SerializeField] private LocalizationKey _localizationKey; 
    [SerializeField] private LocalizationTableSO _localizationTable;
    [SerializeField] private GameSettingsSO _gameSettings;

    [Header("Internal State")]
    private bool _isExclusive;

    private void Start()
    {
        _isExclusive = _gameSettings.IsExclusiveFullscreen;
        _gameSettings.OnLanguageChanged += OnLanguageChanged;

        UpdateVisuals();

        _exclusiveButton.onClick.AddListener(ToggleExclusiveFullscreen);
    }

    private void OnDestroy()
    {
        _exclusiveButton.onClick.RemoveListener(ToggleExclusiveFullscreen);
        _gameSettings.OnLanguageChanged -= OnLanguageChanged;   
    }

    private void ToggleExclusiveFullscreen()
    {
        _isExclusive = !_isExclusive;
        _gameSettings.SetExclusiveFullscreen(_isExclusive);

        UpdateVisuals();

        if (_gameSettings.IsFullscreen)
        {
            Screen.fullScreenMode = _isExclusive ? 
                FullScreenMode.ExclusiveFullScreen : 
                FullScreenMode.FullScreenWindow;
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
            statusText = _isExclusive ? "ON" : "OFF";
        }
        else if (currentLang == GameLanguage.Portuguese)
        {
            statusText = _isExclusive ? "Sim" : "Não";
        }

        string exclusiveText = $"{translatedPrefix}: {statusText}";

        if (_exclusiveLabel != null) _exclusiveLabel.text = exclusiveText;
        if (_exclusiveShadowLabel != null) _exclusiveShadowLabel.text = exclusiveText;
    }
} 
