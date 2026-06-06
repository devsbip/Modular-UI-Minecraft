using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSelectorUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider _resolutionSlider;
    [SerializeField] private TextMeshProUGUI _resolutionLabel;
    [SerializeField] private TextMeshProUGUI _resolutionShadowLabel;

    [Header("Data")]
    [SerializeField] private GameSettingsSO _gameSettings;

    [Header("Internal State")]
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

        PreviewResolution(_resolutionSlider.value);
        _resolutionSlider.onValueChanged.AddListener(PreviewResolution);
    }

    private void ODestroy()
    {
        _resolutionSlider.onValueChanged.RemoveListener(PreviewResolution);
    }

    private void PreviewResolution(float sliderValue)
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
            _pendingResolution = _filteredResolutions[index];

            int width = _pendingResolution.width;
            int height = _pendingResolution.height;
            int heartz = Mathf.RoundToInt((float)_pendingResolution.refreshRateRatio.value);

            UpdateVisuals($"Fullscreen Resolution: {width}x{height} @ {heartz}hz");
        }
    }

    private void UpdateVisuals(string resolutionText)
    {
        if (_resolutionLabel != null) _resolutionLabel.text = resolutionText;
        if (_resolutionShadowLabel != null) _resolutionShadowLabel.text = resolutionText;
    }

    public void ApplyResolution()
    {
        Screen.SetResolution(_pendingResolution.width, _pendingResolution.height, _gameSettings.IsFullscreen);
        _gameSettings.SetResolutionIndex((int)_resolutionSlider.value);
    }
}
