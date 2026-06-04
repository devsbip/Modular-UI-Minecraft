using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private UIPanel _mainMenu;
    [SerializeField] private UIPanel _optionsMenu;
    [SerializeField] private UIPanel _audioMenu;
    [SerializeField] private UIPanel _videoMenu;
    [SerializeField] private UINavigator _uiNavigator;

    [Header("UI Navigation")]
    [SerializeField] private Button _audioBtn;
    [SerializeField] private Button _videoBtn;
    [SerializeField] private Button _languageBtn;
    [SerializeField] private Button _doneBtn;

    [Header("Data Settings")]
    [SerializeField] private GameSettingsSO _gameSettings;

    void Start()
    {
        _audioBtn.onClick.AddListener(OnAudioClicked);
        _videoBtn.onClick.AddListener(OnVideoClicked);
        _languageBtn.onClick.AddListener(OnLanguageClicked);
        _doneBtn.onClick.AddListener(OnDoneClicked);
    }

    void OnDestroy()
    {
        _audioBtn.onClick.RemoveListener(OnAudioClicked);
        _videoBtn.onClick.RemoveListener(OnVideoClicked);
        _languageBtn.onClick.RemoveListener(OnLanguageClicked);
        _doneBtn.onClick.RemoveListener(OnDoneClicked);
    }

    private void OnAudioClicked()
    {
        _uiNavigator.OpenPanel(_audioMenu);
    }

    private void OnVideoClicked()
    {
        _uiNavigator.OpenPanel(_videoMenu);
    }

    private void OnLanguageClicked()
    {
        Debug.Log("Language menu opened...");
    }

    private void OnDoneClicked()
    {
        SettingsIO.Save(_gameSettings);
        _uiNavigator.GoBack();
    }
}
