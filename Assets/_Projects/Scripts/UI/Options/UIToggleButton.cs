using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIToggleButton : MonoBehaviour
{
    [Header("Button References")]
    [SerializeField] private Button _toggleBtn;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private string _prefix;
    [SerializeField] private bool _isOn;

    void Start()
    {
        _toggleBtn.onClick.AddListener(OnToggleClicked);

        UpdateVisuals();
    }

    void OnDestroy()
    {
        _toggleBtn.onClick.RemoveListener(OnToggleClicked);
    }

    private void OnToggleClicked()
    {
        _isOn = !_isOn;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        string status = _isOn ? "ON" : "OFF";
        _title.text = $"{_prefix}: {status}";
    }
}
