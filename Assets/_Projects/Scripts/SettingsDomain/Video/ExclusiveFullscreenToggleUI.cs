using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExclusiveFullscreenToggleUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button _exclusiveButton;
    [SerializeField] private TextMeshProUGUI _exclusiveLabel;
    [SerializeField] private TextMeshProUGUI _exclusiveShadowLabel;

    [Header("Data")]
    [SerializeField] private GameSettingsSO _gameSettings;

    [Header("Internal State")]
    private bool _isExclusive;

    private void Start()
    {
        _isExclusive = _gameSettings.IsExclusiveFullscreen;

        UpdateVisuals();
        _exclusiveButton.onClick.AddListener(ToggleExclusiveFullscreen);
    }

    private void OnDestroy()
    {
        _exclusiveButton.onClick.RemoveListener(ToggleExclusiveFullscreen);
    }

    private void ToggleExclusiveFullscreen()
    {
        _isExclusive = !_isExclusive;
        _gameSettings.SetExclusiveFullscreen(_isExclusive);

        UpdateVisuals();

        if (_gameSettings.IsFullscreen)
        {
            Screen.fullScreenMode = _isExclusive ? 
                FullScreenMode.ExclusiveFullScreen : 
                FullScreenMode.FullScreenWindow;
        }
    }

    private void UpdateVisuals()
    {
        string exclusiveText = _isExclusive ? "Exclusive Fullscreen: ON" : "Exclusive Fullscreen: OFF";

        if (_exclusiveLabel != null) _exclusiveLabel.text = exclusiveText;
        if (_exclusiveShadowLabel != null) _exclusiveShadowLabel.text = exclusiveText;
    }
} 
