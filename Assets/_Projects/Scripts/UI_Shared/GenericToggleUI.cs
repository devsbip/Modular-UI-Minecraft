using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenericToggleUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button _toggleBtn;
    [SerializeField] private TextMeshProUGUI _titleLabel;

    [Header("Settings")]
    [SerializeField] private string _prefix;

    [Header("Internal State")]
    [SerializeField] private bool _isOn;

    private void Start()
    {
        _toggleBtn.onClick.AddListener(ToggleState);
        UpdateVisuals();
    }

    private void OnDestroy()
    {
        _toggleBtn.onClick.RemoveListener(ToggleState);
    }

    private void ToggleState()
    {
        _isOn = !_isOn;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        string status = _isOn ? "ON" : "OFF";
        if (_titleLabel != null) _titleLabel.text = $"{_prefix}: {status}";
    }
}
