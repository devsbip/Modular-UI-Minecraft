using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [Header("Slider References")]
    [SerializeField] private Slider _audioSlider;
    [SerializeField] private TextMeshProUGUI _audioText;

    [Header("Data Settings")]
    [SerializeField] private GameSettingsSO _gameSettings;
    [SerializeField] private AudioChannel _myChannel;

    void Start()
    {
        _audioSlider.onValueChanged.AddListener(OnSliderMoved);

        OnSliderMoved(_audioSlider.value);
    }

    void OnDestroy()
    {
        _audioSlider.onValueChanged.RemoveListener(OnSliderMoved);
    }

    private void OnSliderMoved(float currentValue)
    {
        int displayPercentage = Mathf.RoundToInt(currentValue * 100);
        string channelName = _myChannel.GetUIName();

        if (displayPercentage == 0)
        {
            _audioText.text = $"{channelName}: OFF";
        }
        else
        {
            _audioText.text = $"{channelName}: {displayPercentage}%";
        }

        _gameSettings.UpdateVolume(_myChannel, currentValue);
    }
}
