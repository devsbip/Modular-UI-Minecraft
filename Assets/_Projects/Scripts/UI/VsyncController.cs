using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VsyncController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Button _vsyncButton;
    [SerializeField] private GameSettingsSO _gameSettings;

    [Header("Ui Labels")]
    [SerializeField] private TextMeshProUGUI _vsyncLabel;
    [SerializeField] private TextMeshProUGUI _vsyncShadowLabel;

    private bool _isVsyncEnabled;

    void Start()
    {
        _isVsyncEnabled = _gameSettings.IsVsyncOn;
        UpdateUiText();

        QualitySettings.vSyncCount = _isVsyncEnabled ? 1 : 0;
        _vsyncButton.onClick.AddListener(UpdateVsync);
    }

    void OnDestroy()
    {
        _vsyncButton.onClick.RemoveListener(UpdateVsync);
    }

    private void UpdateVsync()
    {
        _isVsyncEnabled = !_isVsyncEnabled;
        QualitySettings.vSyncCount = _isVsyncEnabled ? 1 : 0;

        _gameSettings.UpdateVsync(_isVsyncEnabled);
        UpdateUiText();
    }

    private void UpdateUiText()
    {
        string vsyncText = _isVsyncEnabled ? "Vsync: ON" : "Vsync: OFF";

        if (_vsyncLabel != null) _vsyncLabel.text = vsyncText;
        if (_vsyncShadowLabel != null) _vsyncShadowLabel.text = vsyncText;
    }
}
