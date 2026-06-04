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
    [SerializeField, Range(0f, 1f)] private float _narratorVolume;
    [SerializeField, Range(0f, 1f)] private float _uiVolume;

    [Header("Video Settings")]
    [SerializeField] private int _graphicsQuality;
    [SerializeField] private int _guiScaleValue;
    [SerializeField] private bool _isFullscreen;
    [SerializeField] private bool _isExclusiveFullscreen;
    [SerializeField] private int _resolutionIndex;
    [SerializeField] private bool _isVsyncOn;

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
    public float NarratorVolume => _narratorVolume;

    // Video Settings
    public int GraphicsQuality => _graphicsQuality;
    public int GuiScale => _guiScaleValue;
    public bool IsFullscreen => _isFullscreen;
    public bool IsExclusiveFullscreen => _isExclusiveFullscreen;
    public int ResolutionIndex => _resolutionIndex;
    public bool IsVsyncOn => _isVsyncOn;

    // Fov Settings
    public int FOV => _fovValue;

    public void UpdateFOV(float fovValue)
    {
        _fovValue = (int)fovValue;
    }

    public void UpdateGuiScale(int guiScale)
    {
        _guiScaleValue = guiScale;
    }

    public void UpdateResolution(int resIndex)
    {
        _resolutionIndex = resIndex;
    }
    
    public void UpdateFullscreen(bool isFullscreen)
    {
        _isFullscreen = isFullscreen;
    }

    public void UpdateExclusiveFullscreen(bool isExclusive)
    {
        _isExclusiveFullscreen = isExclusive;
    }

    public void UpdateVsync(bool isVsyncOn)
    {
        _isVsyncOn = isVsyncOn;
    }

    public float GetVolume(AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Master: return _masterVolume;
            case AudioChannel.Music: return _musicVolume;
            case AudioChannel.Jukebox: return _jukeboxVolume;
            case AudioChannel.Weather: return _weatherVolume;
            case AudioChannel.Blocks: return _blocksVolume;
            case AudioChannel.Hostile: return _hostileVolume;
            case AudioChannel.Friendly: return _friendlyVolume;
            case AudioChannel.Players: return _playersVolume;
            case AudioChannel.Ambient: return _ambientVolume;
            case AudioChannel.Narrator: return _narratorVolume;
            case AudioChannel.UI: return _uiVolume;
            default: return 1f;
        }
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
            case AudioChannel.Players: _playersVolume = volValue; break; 
            case AudioChannel.Ambient: _ambientVolume = volValue; break;
            case AudioChannel.Narrator: _narratorVolume = volValue; break; 
            case AudioChannel.UI: _uiVolume = volValue; break; 
        }
    }
}
