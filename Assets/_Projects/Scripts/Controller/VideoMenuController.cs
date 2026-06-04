using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VideoMenuController : MonoBehaviour
{
    [Header("UI Navigation")]
    [SerializeField] private Button _doneBtn;

    [Header("Dependencies")]
    [SerializeField] private UINavigator _uiNav;
    [SerializeField] private ResolutionController _resController;
    [SerializeField] private GameSettingsSO gameSettings;

    void Start()
    {
        _doneBtn.onClick.AddListener(OnDoneClicked);
    }

    void OnDestroy()
    {
        _doneBtn.onClick.RemoveListener(OnDoneClicked);
    }

    private void OnDoneClicked()
    {
        _resController.ApplyResolution();
        
        SettingsIO.Save(gameSettings);
        _uiNav.GoBack();
    }
}
