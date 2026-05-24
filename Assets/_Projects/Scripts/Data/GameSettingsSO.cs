using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettings", menuName = "Data/Game Settings")]
public class GameSettingsSO : ScriptableObject
{
    [Header("Audio Settings")]
    [SerializeField, Range(0f, 1f)] private float _masterVolume;

    [Header("Video Settings")]
    [SerializeField] private int _graphicsQuality;

    // Audio Configs.
    public float MasterVolume => _masterVolume;

    // Video Settings
    public int GraphicsQuality => _graphicsQuality;
}
