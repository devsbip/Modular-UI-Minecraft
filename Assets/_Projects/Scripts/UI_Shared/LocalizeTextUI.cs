using TMPro;
using UnityEngine;

public class LocalizeTextUI : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private LocalizationKey _localizationKey;
    [SerializeField] private LocalizationTableSO _localizationTable;
    [SerializeField] private GameSettingsSO _gameSettings;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _targetLabel;
    [SerializeField] private TextMeshProUGUI _shadowLabel;

    private void Start()
    {
        UpdateVisuals(_gameSettings.CurrentLanguage);
        _gameSettings.OnLanguageChanged += UpdateVisuals;
    }

    private void ODestroy()
    {
        _gameSettings.OnLanguageChanged -= UpdateVisuals;
    }

    private void UpdateVisuals(GameLanguage newLanguage)
    {
        if (_localizationTable == null || _localizationKey == LocalizationKey.None) 
        {
            return;
        }

        // Busca o texto com total segurança contra erros de digitação (Type-Safe)
        string translatedText = _localizationTable.GetText(_localizationKey, newLanguage);

        if (_targetLabel != null) _targetLabel.text = translatedText;
        if (_shadowLabel != null) _shadowLabel.text = translatedText;
    }
}
