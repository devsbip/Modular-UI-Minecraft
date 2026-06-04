using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExclusiveFullscreenController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Button _exclusiveButton;
    [SerializeField] private GameSettingsSO _gameSettings;

    [Header("Ui Labels")]
    [SerializeField] private TextMeshProUGUI _exclusiveLabel;
    [SerializeField] private TextMeshProUGUI _exclusiveShadowLabel;

    private bool _isExclusive;

    private void Start()
    {
        _isExclusive = _gameSettings.IsExclusiveFullscreen;

        UpdateUiText();
        _exclusiveButton.onClick.AddListener(ExclusiveFullscreenUpdate);
    }

    void OnDestroy()
    {
        _exclusiveButton.onClick.RemoveListener(ExclusiveFullscreenUpdate);
    }

    private void ExclusiveFullscreenUpdate()
    {
        _isExclusive = !_isExclusive;
        _gameSettings.UpdateExclusiveFullscreen(_isExclusive);

        UpdateUiText();

        if (_gameSettings.IsFullscreen)
        {
            if (_isExclusive) Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            else Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
    }

    private void UpdateUiText()
    {
        string exclusiveText = _isExclusive ? "Exclusive Fullscreen: ON" : "Exclusive Fullscreen: OFF";

        if (_exclusiveLabel != null) _exclusiveLabel.text = exclusiveText;
        if (_exclusiveShadowLabel != null) _exclusiveShadowLabel.text = exclusiveText;
    }
} 
