using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIScaleButtonUI : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private CanvasScaler _canvasScaler;

    [Header("UI Elements")]
    [SerializeField] private Button _scaleButton;
    [SerializeField] private TextMeshProUGUI _scaleLabel;
    [SerializeField] private TextMeshProUGUI _scaleShadowLabel;

    [Header("Settings")]
    [SerializeField] private int _minAllowedScaleLimit = 1;
    [SerializeField] private int _maxAllowedScaleLimit = 6;
    [SerializeField] private float _minSafeScreenHeight = 240f;

    [Header("Data")]
    [SerializeField] private GameSettingsSO _gameSettings;

    [Header("Internal State")]
    private int _currentScaleFactor;
    private float _lastScreenHeight;

    private void Start()
    {
        _currentScaleFactor = _gameSettings.GuiScale;
        ValidateAndApplyScale(_currentScaleFactor);
        _lastScreenHeight = Screen.height;

        _scaleButton.onClick.AddListener(CycleGuiScale);
    }

    private void Update()
    {
        if (Mathf.Abs(Screen.height - _lastScreenHeight) > 1f)
        {
            _lastScreenHeight = Screen.height;
            ValidateAndApplyScale(_currentScaleFactor);
        }
    }

    private void OnDestroy()
    {
        _scaleButton.onClick.RemoveListener(CycleGuiScale);
    }

    private void CycleGuiScale()
    {
        int maxAllowed = CalculateMaxAllowedScale();
        _currentScaleFactor++;

        if (_currentScaleFactor > maxAllowed)
        {
            _currentScaleFactor = _minAllowedScaleLimit;
        }

        ValidateAndApplyScale(_currentScaleFactor);
    }

    private int CalculateMaxAllowedScale()
    {
        int roundHeight = Mathf.FloorToInt(Screen.height / _minSafeScreenHeight);
        return Mathf.Clamp(roundHeight, _minAllowedScaleLimit, _maxAllowedScaleLimit);
    }

    private void ValidateAndApplyScale(int targetScale)
    {
        int maxAllowed = CalculateMaxAllowedScale();
        _currentScaleFactor = Mathf.Clamp(targetScale, _minAllowedScaleLimit, maxAllowed);

        if (_canvasScaler != null)
        {
            _canvasScaler.scaleFactor = _currentScaleFactor;
        }

        UpdateVisuals();
        _gameSettings.SetGuiScale(_currentScaleFactor);
    }

    private void UpdateVisuals()
    {
        string scaleText = $"GUI Scale: {_currentScaleFactor}";

        if (_scaleLabel != null) _scaleLabel.text = scaleText;
        if (_scaleShadowLabel != null) _scaleShadowLabel.text = scaleText;
    }
}
