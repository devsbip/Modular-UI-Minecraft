using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettings", menuName = "Data/Game Settings")]
public class GameSettingsSO : ScriptableObject
{
    [Header("Audio Data")]
    [SerializeField, Range(0f, 1f)] private float _masterVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float _musicVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float _jukeboxVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float _weatherVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float _blocksVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float _hostileVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float _friendlyVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float _playersVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float _ambientVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float _narratorVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float _uiVolume = 1f;

    [Header("Video Settings")]
    [SerializeField] private int _graphicsQuality;
    [SerializeField] private int _guiScaleValue = 2;
    [SerializeField] private bool _isFullscreen = false;
    [SerializeField] private bool _isExclusiveFullscreen;
    [SerializeField] private int _resolutionIndex;
    [SerializeField] private bool _isVsyncOn;
    [SerializeField] private int _maxFramerate;
    [SerializeField] private BackgroundFpsMode _backgroundFpsMode;

    [Header("Gameplay Data")]
    [SerializeField] private int _fovValue = 70;

    [Header("Localization")]
    [SerializeField] private GameLanguage _currentLanguage = GameLanguage.English;

    
    // Events
    public event Action<AudioChannel, float> OnVolumeChanged;
    public event Action<GameLanguage> OnLanguageChanged;

    // --- Getters ---

    // Audio
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
    public float UiVolume => _uiVolume;

    // Video
    public int GraphicsQuality => _graphicsQuality;
    public int GuiScale => _guiScaleValue;
    public bool IsFullscreen => _isFullscreen;
    public bool IsExclusiveFullscreen => _isExclusiveFullscreen;
    public int ResolutionIndex => _resolutionIndex;
    public bool IsVsyncOn => _isVsyncOn;
    public int MaxFramerate => _maxFramerate;
    public int FOV => _fovValue;
    public BackgroundFpsMode BackgroundMode => _backgroundFpsMode;

    // Localization
    public GameLanguage CurrentLanguage => _currentLanguage;

    // --- Setters ---
    public void SetFOV(float fovValue)
    {
        _fovValue = (int)fovValue;
    }

    public void SetGuiScale(int guiScale)
    {
        _guiScaleValue = guiScale;
    }

    public void SetResolutionIndex(int resolutionIndex)
    {
        _resolutionIndex = resolutionIndex;
    }
    
    public void SetFullscreenMode(bool isFullscreen)
    {
        _isFullscreen = isFullscreen;
    }

    public void SetExclusiveFullscreen(bool isExclusive)
    {
        _isExclusiveFullscreen = isExclusive;
    }

    public void SetVsync(bool isVsyncOn)
    {
        _isVsyncOn = isVsyncOn;
    }

    public void SetFramerate(int framerateValue)
    {
        _maxFramerate = framerateValue;
    }

    public void SetBackgroundFpsMode(BackgroundFpsMode backgroundFpsMode)
    {
        _backgroundFpsMode = backgroundFpsMode;
    }

    public void SetLanguage(GameLanguage language)
    {
        _currentLanguage = language;
        OnLanguageChanged?.Invoke(_currentLanguage);
    }

    public float GetVolume(AudioChannel channel)
    {
        return channel switch
        {
            AudioChannel.Master => _masterVolume,
            AudioChannel.Music => _musicVolume,
            AudioChannel.Jukebox => _jukeboxVolume,
            AudioChannel.Weather => _weatherVolume,
            AudioChannel.Blocks => _blocksVolume,
            AudioChannel.Hostile => _hostileVolume,
            AudioChannel.Friendly => _friendlyVolume,
            AudioChannel.Players => _playersVolume,
            AudioChannel.Ambient => _ambientVolume,
            AudioChannel.Narrator => _narratorVolume,
            AudioChannel.UI => _uiVolume,
            _ => 1f
        };
    }

    public void SetVolume(AudioChannel channel, float volumeValue)
    {
        switch (channel)
        {
            case AudioChannel.Master: _masterVolume = volumeValue; break;
            case AudioChannel.Music: _musicVolume = volumeValue; break;
            case AudioChannel.Jukebox: _jukeboxVolume = volumeValue; break;
            case AudioChannel.Weather: _weatherVolume = volumeValue; break;
            case AudioChannel.Blocks: _blocksVolume = volumeValue; break;
            case AudioChannel.Hostile: _hostileVolume = volumeValue; break;
            case AudioChannel.Friendly: _friendlyVolume = volumeValue; break;
            case AudioChannel.Players: _playersVolume = volumeValue; break; 
            case AudioChannel.Ambient: _ambientVolume = volumeValue; break;
            case AudioChannel.Narrator: _narratorVolume = volumeValue; break; 
            case AudioChannel.UI: _uiVolume = volumeValue; break; 
        }

        OnVolumeChanged?.Invoke(channel, volumeValue);
    }
}
