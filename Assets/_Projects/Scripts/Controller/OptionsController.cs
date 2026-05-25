using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [Header("Menus References")]
    [SerializeField] private UIPanel _mainMenu;
    [SerializeField] private UIPanel _optionsMenu;
    [SerializeField] private UIPanel _audioMenu;

    [SerializeField] private UINavigator _uiNavigator;

    [Header("Buttons References")]
    [SerializeField] private Button _audioBtn;
    [SerializeField] private Button _videoBtn;
    [SerializeField] private Button _languageBtn;
    [SerializeField] private Button _doneBtn;

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
        Debug.Log("Video menu opened...");
    }

    private void OnLanguageClicked()
    {
        Debug.Log("Language menu opened...");
    }

    private void OnDoneClicked()
    {
        _uiNavigator.GoBack();
    }
}
