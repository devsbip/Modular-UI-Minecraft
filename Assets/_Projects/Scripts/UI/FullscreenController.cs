using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Button _fullscreenButton;
    [SerializeField] private GameSettingsSO _gameSettings;

    [Header("Labels")]
    [SerializeField] private TextMeshProUGUI _fullscreenLabel;
    [SerializeField] private TextMeshProUGUI _fullscreenShadowLabel;
    private bool _isFullscreen;

    private void Start()
    {
        _fullscreenButton.onClick.AddListener(OnFullscreenClicked);
        
        if (_gameSettings != null) _isFullscreen = _gameSettings.IsFullscreen;
        ApplyFullscreenState();

        UpdateUiTexts();
    }

    private void OnDestroy()
    {
        _fullscreenButton.onClick.RemoveListener(OnFullscreenClicked);
    }

    private void OnFullscreenClicked()
    {
        _isFullscreen = !_isFullscreen;

        ApplyFullscreenState();

        _gameSettings.UpdateFullscreen(_isFullscreen);
        UpdateUiTexts();
    }

    private void ApplyFullscreenState()
    {
        if (_isFullscreen)
        {
            if (_gameSettings.IsExclusiveFullscreen)
            {
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            }
            else
            {
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            }
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    private void UpdateUiTexts()
    {
        string displayText = _isFullscreen ? "Fullscreen: ON" : "Fullscreen: OFF";

        if (_fullscreenLabel != null) _fullscreenLabel.text = displayText;
        if (_fullscreenShadowLabel != null) _fullscreenShadowLabel.text = displayText;
    }
}
