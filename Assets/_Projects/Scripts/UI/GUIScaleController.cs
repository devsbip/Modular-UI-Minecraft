using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIScaleController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Button _scaleBtn;
    [SerializeField] private CanvasScaler _canvasScaler;
    [SerializeField] private GameSettingsSO _gameSettings;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _scaleLabel;
    [SerializeField] private TextMeshProUGUI _scaleShadowLabel;

    [Header("Scale Settings")]
    [SerializeField] private int _minAllowedScaleLimit = 1;
    [SerializeField] private int _maxAllowedScaleLimit = 6;
    [SerializeField] private float _minSafeScreenHeight;

    [Header("Debug")]
    [SerializeField] private int _currentScaleFactor;

    private float _lastScreenHeight;

    void Start()
    {
        _scaleBtn.onClick.AddListener(CycleToNextScale);

        _currentScaleFactor = _gameSettings.GuiScale;
        ValidateAndApplyScale(_currentScaleFactor);

        _lastScreenHeight = Screen.height;
    }

    void Update()
    {
        if (Mathf.Abs(Screen.height - _lastScreenHeight) > 1f)
        {
            _lastScreenHeight = Screen.height;
            ValidateAndApplyScale(_currentScaleFactor);
        }
    }

    void OnDestroy()
    {
        _scaleBtn.onClick.RemoveListener(CycleToNextScale);
    }

    private void CycleToNextScale()
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

        UpdateUiText();
        SaveScaleToSettings();
    }

    private void UpdateUiText()
    {
        string newText = $"GUI Scale: {_currentScaleFactor}";

        if (_scaleLabel != null)
        {
            _scaleLabel.text = newText;
        }

        if (_scaleShadowLabel != null)
        {
            _scaleShadowLabel.text = newText;
        }
    }

    private void SaveScaleToSettings()
    {
        _gameSettings.UpdateGuiScale(_currentScaleFactor);
    }
}
