using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenericToggleUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button _toggleBtn;
    [SerializeField] private TextMeshProUGUI _titleLabel;
    [SerializeField] private TextMeshProUGUI _titleShadowLabel;

    [Header("Data")]
    [SerializeField] private LocalizationKey _localizationKey;
    [SerializeField] private LocalizationTableSO _localizationTable;
    [SerializeField] private GameSettingsSO _gameSettings;

    [Header("Internal State")]
    [SerializeField] private bool _isOn;

    private void Start()
    {
        _toggleBtn.onClick.AddListener(ToggleState);

        _gameSettings.OnLanguageChanged += OnLanguageChanged;
        UpdateVisuals();
    }

    private void OnDestroy()
    {
        _toggleBtn.onClick.RemoveListener(ToggleState);
        _gameSettings.OnLanguageChanged -= OnLanguageChanged;
    }

    private void OnLanguageChanged(GameLanguage newLanguage)
    {
        UpdateVisuals();
    }

    private void ToggleState()
    {
        _isOn = !_isOn;
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
            statusText = _isOn ? "On" : "Off";
        }else if (currentLang == GameLanguage.Portuguese)
        {
            statusText = _isOn ? "Sim" : "Não";
        }

        string displayText = $"{translatedPrefix}: {statusText}";
        if (_titleLabel != null) _titleLabel.text = displayText;
        if (_titleShadowLabel != null) _titleShadowLabel.text = displayText;
    }
}
