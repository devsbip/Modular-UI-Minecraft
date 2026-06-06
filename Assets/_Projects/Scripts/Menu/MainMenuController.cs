using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private UIPanel _mainMenu;
    [SerializeField] private UIPanel _optionsMenu;
    [SerializeField] private UINavigator _uiNavigator;

    [Header("UI Elements")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _quitButton;

    [Header("Data")]
    [SerializeField] private GameSettingsSO _gameSettings;

    private void Awake()
    {
        SettingsIO.Load(_gameSettings);
    }

    private void Start()
    {
        _uiNavigator.OpenPanel(_mainMenu);

        _playButton.onClick.AddListener(StartGame);
        _optionsButton.onClick.AddListener(OpenOptionsMenu);
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(StartGame);
        _optionsButton.onClick.RemoveListener(OpenOptionsMenu);
        _quitButton.onClick.RemoveListener(QuitGame);
    }

    private void StartGame()
    {
        Debug.Log("Game Starting...");
    }

    private void OpenOptionsMenu()
    {
        _uiNavigator.OpenPanel(_optionsMenu);
    }

    private void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Closing...");
    }
}
