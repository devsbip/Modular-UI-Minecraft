using UnityEngine;
using UnityEngine.UI;

public class VideoMenuController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private UINavigator _uiNavigator;
    [SerializeField] private ResolutionSelectorUI _resolutionController;

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
        _resolutionController.ApplyResolution();
        
        SettingsIO.Save(_gameSettings);
        _uiNavigator.GoBack();
    }
}
