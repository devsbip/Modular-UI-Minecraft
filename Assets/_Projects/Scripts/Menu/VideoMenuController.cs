using UnityEngine;
using UnityEngine.UI;

public class VideoMenuController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private UINavigator _uiNavigator;
    [SerializeField] private ResolutionSelectorUI _resolutionSelector;
    [SerializeField] private FramerateSliderUI _framerateSliderUI;
 
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
        _resolutionSelector.ApplyResolution();
        _framerateSliderUI.ConfirmFramerateSettings();
        
        SettingsIO.Save(_gameSettings);
        _uiNavigator.GoBack();
    }
}
