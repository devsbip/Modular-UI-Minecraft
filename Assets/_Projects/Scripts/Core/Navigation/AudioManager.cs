using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private AudioMixer _mainMixer;
    [SerializeField] private GameSettingsSO _gameSettings;

    [Header("Audio Elements")]
    [SerializeField] private AudioSource _uiAudioSource;
    [SerializeField] private AudioClip _uiClickClip;

    private void OnEnable()
    {
        _gameSettings.OnVolumeChanged += ApplyVolumeToMixer;
    }

    private void OnDisable()
    {
        _gameSettings.OnVolumeChanged -= ApplyVolumeToMixer;
    }

    private void Start()
    {
        ApplyVolumeToMixer(AudioChannel.Master, _gameSettings.MasterVolume);
        ApplyVolumeToMixer(AudioChannel.Music, _gameSettings.MusicVolume);
        ApplyVolumeToMixer(AudioChannel.UI, _gameSettings.UiVolume);
    }

    public void PlayUIClick()
    {
        if (_uiAudioSource != null && _uiClickClip != null)
        {
            _uiAudioSource.PlayOneShot(_uiClickClip);
        }
    }

    private void ApplyVolumeToMixer(AudioChannel channel, float linearVolume)
    {
        string exposedParameter = GetExposedParameterName(channel);
        if (string.IsNullOrEmpty(exposedParameter)) return;

        float decibelVolume = ConvertLinearToDecibel(linearVolume);
        _mainMixer.SetFloat(exposedParameter, decibelVolume);
    }

    private float ConvertLinearToDecibel(float linear)
    {
        if (linear <= 0.0001f) return -80f;
        return Mathf.Log10(linear) * 20f;
    }

    private string GetExposedParameterName(AudioChannel channel)
    {
        return channel switch
        {
            AudioChannel.Master => "MasterVolume",
            AudioChannel.Music => "MusicVolume",
            AudioChannel.UI => "UiVolume",
            _ => ""
        };
    }
}
