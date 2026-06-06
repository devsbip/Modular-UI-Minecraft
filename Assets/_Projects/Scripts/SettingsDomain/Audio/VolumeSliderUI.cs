using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider _audioSlider;
    [SerializeField] private TextMeshProUGUI _audioLabel;
    [SerializeField] private TextMeshProUGUI _audioShadowLabel;

    [Header("Data")]
    [SerializeField] private GameSettingsSO _gameSettings;

    [Header("Settings")]
    [SerializeField] private AudioChannel _myChannel;

    private void Start()
    {
        float savedVolume = _gameSettings.GetVolume(_myChannel);
        _audioSlider.value = savedVolume;

        UpdateVisuals(savedVolume);

        _audioSlider.onValueChanged.AddListener(ApplyVolume);
    }

    private void OnDestroy()
    {
        _audioSlider.onValueChanged.RemoveListener(ApplyVolume);
    }

    private void ApplyVolume(float currentValue)
    {
        UpdateVisuals(currentValue);
        _gameSettings.SetVolume(_myChannel, currentValue);
    }

    private void UpdateVisuals(float currentValue)
    {
        int displayPercentage = Mathf.RoundToInt(currentValue * 100);
        string channelName = _myChannel.GetUIName();

        string displayText = displayPercentage == 0 
            ? $"{channelName}: OFF" 
            : $"{channelName}: {displayPercentage}%";

        if (_audioLabel != null) _audioLabel.text = displayText;
        if (_audioShadowLabel != null) _audioShadowLabel.text = displayText;
    }
}
