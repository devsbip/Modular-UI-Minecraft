using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private UIPanel _mainMenu;
    [SerializeField] private UIPanel _optionsMenu;
    [SerializeField] private UIPanel _audioMenu;
    [SerializeField] private UIPanel _videoMenu;
    [SerializeField] private UINavigator _uiNavigator;

    [Header("UI Elements")]
    [SerializeField] private Button _audioButton;
    [SerializeField] private Button _videoButton;
    [SerializeField] private Button _languageButton;
    [SerializeField] private Button _doneButton;

    [Header("Data")]
    [SerializeField] private GameSettingsSO _gameSettings;

    private void Start()
    {
        _audioButton.onClick.AddListener(OpenAudioMenu);
        _videoButton.onClick.AddListener(OpenVideoMenu);
        _languageButton.onClick.AddListener(OpenLanguageMenu);
        _doneButton.onClick.AddListener(SaveAndExitMenu);
    }

    private void OnDestroy()
    {
        _audioButton.onClick.RemoveListener(OpenAudioMenu);
        _videoButton.onClick.RemoveListener(OpenVideoMenu);
        _languageButton.onClick.RemoveListener(OpenLanguageMenu);
        _doneButton.onClick.RemoveListener(SaveAndExitMenu);
    }

    private void OpenAudioMenu()
    {
        _uiNavigator.OpenPanel(_audioMenu);
    }

    private void OpenVideoMenu()
    {
        _uiNavigator.OpenPanel(_videoMenu);
    }

    private void OpenLanguageMenu()
    {
        Debug.Log("Language menu opened...");
    }

    private void SaveAndExitMenu()
    {
        SettingsIO.Save(_gameSettings);
        _uiNavigator.GoBack();
    }
}
