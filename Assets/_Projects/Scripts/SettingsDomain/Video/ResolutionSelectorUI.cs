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
    [SerializeField] private LocalizationKey _localizationKey;
    [SerializeField] private LocalizationTableSO _localizationTable;
    [SerializeField] private GameSettingsSO _gameSettings;

    [Header("Internal State")]
    private List<Resolution> _filteredResolutions = new List<Resolution>();
    private Resolution _pendingResolution;

    private void Start()
    {
        Resolution[] nativeResolutions = Screen.resolutions;
        System.Array.Reverse(nativeResolutions);

        Resolution desktopResolution = Screen.currentResolution;

        foreach (Resolution currentRes in nativeResolutions)
        {
            float aspectRatio = (float)currentRes.width / (float)currentRes.height;
            float targetAspectRatio = 16f / 9f;
            float margin = .05f;

            if (Mathf.Abs(aspectRatio - targetAspectRatio) <= margin)
            {
                bool isSameWidth = currentRes.width == desktopResolution.width;
                bool isSameHeight = currentRes.height == desktopResolution.height;
                
                bool isSameRefreshRate = Mathf.RoundToInt((float)currentRes.refreshRateRatio.value) == Mathf.RoundToInt((float)desktopResolution.refreshRateRatio.value);

                if (isSameWidth && isSameHeight && isSameRefreshRate)
                {
                    continue; 
                }

                _filteredResolutions.Add(currentRes);
            }
        }

        _resolutionSlider.maxValue = _filteredResolutions.Count;
        _resolutionSlider.value = _gameSettings.ResolutionIndex;

        _gameSettings.OnLanguageChanged += OnLanguageChanged;

        PreviewResolution(_resolutionSlider.value);
        _resolutionSlider.onValueChanged.AddListener(PreviewResolution);
    }

    private void OnDestroy()
    {
        _resolutionSlider.onValueChanged.RemoveListener(PreviewResolution);
        _gameSettings.OnLanguageChanged -= OnLanguageChanged;
    }

    private void PreviewResolution(float sliderValue)
    {
        
        if (sliderValue == 0)
        {
            _pendingResolution = Screen.currentResolution;
        }
        else
        {
            int index = (int)sliderValue - 1;
            _pendingResolution = _filteredResolutions[index];
        }

        UpdateVisuals();
    }

    private void OnLanguageChanged(GameLanguage newLanguage)
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (_localizationTable == null || _gameSettings == null || _localizationKey == LocalizationKey.None) return;

        GameLanguage currentLang = _gameSettings.CurrentLanguage;

        string translatedPrefix = _localizationTable.GetText(_localizationKey, currentLang);
        string statusText;

        if (_resolutionSlider.value == 0)
        {
            statusText = currentLang == GameLanguage.Portuguese ? "Atual" : "Current";
        }else
        {
            int width = _pendingResolution.width;
            int height = _pendingResolution.height;
            int hertz = Mathf.RoundToInt((float)_pendingResolution.refreshRateRatio.value);

            statusText = $"{width}x{height} @ {hertz}hz";
        }

        string displayText = $"{translatedPrefix}: {statusText}";
        if (_resolutionLabel != null) _resolutionLabel.text = displayText;
        if (_resolutionShadowLabel != null) _resolutionShadowLabel.text = displayText;
    }

    public void ApplyResolution()
    {
        Screen.SetResolution(_pendingResolution.width, _pendingResolution.height, _gameSettings.IsFullscreen);
        _gameSettings.SetResolutionIndex((int)_resolutionSlider.value);
    }
}
