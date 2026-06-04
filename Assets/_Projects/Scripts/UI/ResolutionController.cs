using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Slider _resolutionSlider;
    [SerializeField] private GameSettingsSO _gameSettings;

    [Header("Ui Labels")]
    [SerializeField] private TextMeshProUGUI _resolutionLabel;
    [SerializeField] private TextMeshProUGUI _resolutionShadowLabel;

    private List<Resolution> _filteredResolutions = new List<Resolution>();
    private Resolution _pendingResolution;

    private void Start()
    {
        Resolution[] nativeResolutions = Screen.resolutions;
        System.Array.Reverse(nativeResolutions);

        foreach (Resolution currentRes in nativeResolutions)
        {
            float aspectRatio = (float)currentRes.width / (float)currentRes.height;

            float targetAspectRatio = 16f / 9f;
            float margin = .05f;

            if (Mathf.Abs(aspectRatio - targetAspectRatio) <= margin)
            {
                _filteredResolutions.Add(currentRes);
            }
        }

        _resolutionSlider.maxValue = _filteredResolutions.Count;
        _resolutionSlider.value = _gameSettings.ResolutionIndex;
        OnSliderMoved(_resolutionSlider.value);

        _resolutionSlider.onValueChanged.AddListener(OnSliderMoved);
    }

    private void OnSliderMoved(float sliderValue)
    {
        
        if (sliderValue == 0)
        {
            _pendingResolution = Screen.currentResolution;

            _resolutionLabel.text = "Fullscreen Resolution: Current";
            _resolutionShadowLabel.text = "Fullscreen Resolution: Current";
        }
        else
        {
            int index = (int)sliderValue - 1;
            Resolution targetResolution = _filteredResolutions[index];
            _pendingResolution = targetResolution;

            int width = targetResolution.width;
            int height = targetResolution.height;
            int hz = Mathf.RoundToInt((float)targetResolution.refreshRateRatio.value);

            string resolutionText = $"Fullscreen Resolution: {width}x{height} @ {hz}Hz";

            if (_resolutionLabel != null) _resolutionLabel.text = resolutionText;
            if (_resolutionShadowLabel != null) _resolutionShadowLabel.text = resolutionText;
        }
    }

    public void ApplyResolution()
    {
        Screen.SetResolution(_pendingResolution.width, _pendingResolution.height, _gameSettings.IsFullscreen);

        _gameSettings.UpdateResolution((int)_resolutionSlider.value);
    }
}
