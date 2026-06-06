using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VsyncToggleUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button _vsyncButton;
    [SerializeField] private TextMeshProUGUI _vsyncLabel;
    [SerializeField] private TextMeshProUGUI _vsyncShadowLabel;

    [Header("Data")]
    [SerializeField] private GameSettingsSO _gameSettings;

    [Header("Internal State")]
    private bool _isVsyncEnabled;

    private void Start()
    {
        _isVsyncEnabled = _gameSettings.IsVsyncOn;
        UpdateVisuals();

        QualitySettings.vSyncCount = _isVsyncEnabled ? 1 : 0;
        _vsyncButton.onClick.AddListener(ToggleVsync);
    }

    private void OnDestroy()
    {
        _vsyncButton.onClick.RemoveListener(ToggleVsync);
    }

    private void ToggleVsync()
    {
        _isVsyncEnabled = !_isVsyncEnabled;
        QualitySettings.vSyncCount = _isVsyncEnabled ? 1 : 0;

        _gameSettings.SetVsync(_isVsyncEnabled);
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        string vsyncText = _isVsyncEnabled ? "Vsync: ON" : "Vsync: OFF";

        if (_vsyncLabel != null) _vsyncLabel.text = vsyncText;
        if (_vsyncShadowLabel != null) _vsyncShadowLabel.text = vsyncText;
    }
}
