using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Slider _audioSlider;
    [SerializeField] private TextMeshProUGUI _audioLabel;
    [SerializeField] private TextMeshProUGUI _audioShadowLabel;

    [Header("Data Settings")]
    [SerializeField] private GameSettingsSO _gameSettings;
    [SerializeField] private AudioChannel _myChannel;

    void Start()
    {
        float savedVol = _gameSettings.GetVolume(_myChannel);
        _audioSlider.value = savedVol;

        UpdateVisualTexts(savedVol);

        _audioSlider.onValueChanged.AddListener(OnSliderMoved);
    }

    void OnDestroy()
    {
        _audioSlider.onValueChanged.RemoveListener(OnSliderMoved);
    }

    private void OnSliderMoved(float currentValue)
    {
        UpdateVisualTexts(currentValue);

        _gameSettings.UpdateVolume(_myChannel, currentValue);
    }

    private void UpdateVisualTexts(float currentValue)
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
