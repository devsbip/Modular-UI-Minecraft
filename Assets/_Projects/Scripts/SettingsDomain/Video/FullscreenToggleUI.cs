using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggleUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button _fullscreenButton;
    [SerializeField] private TextMeshProUGUI _fullscreenLabel;
    [SerializeField] private TextMeshProUGUI _fullscreenShadowLabel;

    [Header("Data")]
    [SerializeField] private GameSettingsSO _gameSettings;

    [Header("Internal State")]
    private bool _isFullscreen;

    private void Start()
    {
        if (_gameSettings != null) _isFullscreen = _gameSettings.IsFullscreen;

        ApplyFullscreenState();
        UpdateVisuals();

        _fullscreenButton.onClick.AddListener(ToggleFullscreen); 
    }

    private void OnDestroy()
    {
        _fullscreenButton.onClick.RemoveListener(ToggleFullscreen);
    }

    private void ToggleFullscreen()
    {
        _isFullscreen = !_isFullscreen;

        ApplyFullscreenState();
        _gameSettings.SetFullscreenMode(_isFullscreen);
        UpdateVisuals();
    }

    private void ApplyFullscreenState()
    {
        if (_isFullscreen)
        {
            Screen.fullScreenMode = _gameSettings.IsExclusiveFullscreen ? 
                FullScreenMode.ExclusiveFullScreen : 
                FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    private void UpdateVisuals()
    {
        string displayText = _isFullscreen ? "Fullscreen: ON" : "Fullscreen: OFF";

        if (_fullscreenLabel != null) _fullscreenLabel.text = displayText;
        if (_fullscreenShadowLabel != null) _fullscreenShadowLabel.text = displayText;
    }
}
