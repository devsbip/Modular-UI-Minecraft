using UnityEngine;
using UnityEngine.UI;

public class AudioMenuController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Button _doneBtn;
    [SerializeField] private UINavigator _uiNav;

    [Header("Data")]
    [SerializeField] private GameSettingsSO _gameSettings;

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
        SettingsIO.Save(_gameSettings);
        _uiNav.GoBack();
    }
}
