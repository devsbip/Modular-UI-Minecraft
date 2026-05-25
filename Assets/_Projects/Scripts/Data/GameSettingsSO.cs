using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettings", menuName = "Data/Game Settings")]
public class GameSettingsSO : ScriptableObject
{
    [Header("Audio Settings")]
    [SerializeField, Range(0f, 1f)] private float _masterVolume;
    [SerializeField, Range(0f, 1f)] private float _musicVolume;
    [SerializeField, Range(0f, 1f)] private float _jukeboxVolume;
    [SerializeField, Range(0f, 1f)] private float _weatherVolume;
    [SerializeField, Range(0f, 1f)] private float _blocksVolume;
    [SerializeField, Range(0f, 1f)] private float _hostileVolume;
    [SerializeField, Range(0f, 1f)] private float _friendlyVolume;
    [SerializeField, Range(0f, 1f)] private float _playersVolume;
    [SerializeField, Range(0f, 1f)] private float _ambientVolume;
    [SerializeField, Range(0f, 1f)] private float _voiceVolume;

    [Header("Video Settings")]
    [SerializeField] private int _graphicsQuality;

    [Header("Options Menu")]
    [SerializeField] private int _fovValue;

    // Audio Configs.
    public float MasterVolume => _masterVolume;
    public float MusicVolume => _musicVolume;
    public float JukeboxVolume => _jukeboxVolume;
    public float WeatherVolume => _weatherVolume;
    public float BlocksVolume => _blocksVolume;
    public float HostileVolume => _hostileVolume;
    public float FriendlyVolume => _friendlyVolume;
    public float PlayersVolume => _playersVolume;
    public float AmbientVolume => _ambientVolume;
    public float VoiceVolume => _voiceVolume;

    // Video Settings
    public int GraphicsQuality => _graphicsQuality;

    // Fov Settings
    public int FOV => _fovValue;

    public void UpdateFOV(float fovValue)
    {
        _fovValue = (int)fovValue;
    }

    public void UpdateVolume(AudioChannel channel, float volValue)
    {
        switch (channel)
        {
            case AudioChannel.Master: _masterVolume = volValue; break;
            case AudioChannel.Music: _musicVolume = volValue; break;
            case AudioChannel.Jukebox: _jukeboxVolume = volValue; break;
            case AudioChannel.Weather: _weatherVolume = volValue; break;
            case AudioChannel.Blocks: _blocksVolume = volValue; break;
            case AudioChannel.Hostile: _hostileVolume = volValue; break;
            case AudioChannel.Friendly: _friendlyVolume = volValue; break;
            case AudioChannel.Ambient: _ambientVolume = volValue; break;
            case AudioChannel.Voice: _ambientVolume = volValue; break;
        }
    }
}
