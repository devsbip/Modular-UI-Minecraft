using UnityEngine;
using UnityEngine.UI;

public class AudioMenuController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private UINavigator _uiNav;
    
    [Header("UI Elements")]
    [SerializeField] private Button _doneButton;

    [Header("Data")]
    [SerializeField] private GameSettingsSO _gameSettings;

    private void Start()
    {
        _doneButton.onClick.AddListener(SaveAndExitMenu);
    }

    private void OnDestroy()
    {
        _doneButton.onClick.RemoveListener(SaveAndExitMenu);
    }

    private void SaveAndExitMenu()
    {
        SettingsIO.Save(_gameSettings);
        _uiNav.GoBack();
    }
}
