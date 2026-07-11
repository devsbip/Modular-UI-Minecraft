using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LanguageToggleButtonUI : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private GameSettingsSO _gameSettings;

    private Button _languageButton;

    private void Awake()
    {
        _languageButton = GetComponent<Button>();
    }

    private void Start()
    {
        _languageButton.onClick.AddListener(ToggleLanguage);
    }

    private void OnDestroy()
    {
        _languageButton.onClick.RemoveListener(ToggleLanguage);
    }

    private void ToggleLanguage()
    {
        if (_gameSettings == null) return;

        GameLanguage newLanguage = _gameSettings.CurrentLanguage == GameLanguage.English
            ? GameLanguage.Portuguese
            : GameLanguage.English;

        _gameSettings.SetLanguage(newLanguage);
    }
}
