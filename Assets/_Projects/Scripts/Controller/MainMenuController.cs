using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private UIPanel _mainMenu;
    [SerializeField] private UIPanel _optionsMenu;
    [SerializeField] private UINavigator _uiNavigator;

    [Header("UI Navigation")]
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _optionsBtn;
    [SerializeField] private Button _quitBtn;

    [Header("Data")]
    [SerializeField] private GameSettingsSO _gameSettings;

    void Awake()
    {
        SettingsIO.Load(_gameSettings);
    }

    void Start()
    {
        _uiNavigator.OpenPanel(_mainMenu);

        // Listeners
        _playBtn.onClick.AddListener(OnPlayClicked);
        _optionsBtn.onClick.AddListener(OnOptionsClicked);
        _quitBtn.onClick.AddListener(OnQuitClicked);
    }

    void OnDestroy()
    {
        _playBtn.onClick.RemoveListener(OnPlayClicked);
        _optionsBtn.onClick.RemoveListener(OnOptionsClicked);
        _quitBtn.onClick.RemoveListener(OnQuitClicked);
    }

    public void OnPlayClicked()
    {
        Debug.Log("Game Starting...");
    }

    public void OnOptionsClicked()
    {
        _uiNavigator.OpenPanel(_optionsMenu);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
        Debug.Log("Game Closing...");
    }
}
