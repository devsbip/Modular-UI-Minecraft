using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FOVSlider : MonoBehaviour
{
    [Header("Fov References")]
    [SerializeField] private Slider _fovSlider;
    [SerializeField] private TextMeshProUGUI _fovText;
    [SerializeField] private TextMeshProUGUI _fovShadow;

    [Header("Game Data")]
    [SerializeField] private GameSettingsSO _gameSettings;

    void Start()
    {
        _fovSlider.onValueChanged.AddListener(OnFOVChanged);

        OnFOVChanged(_fovSlider.value);
    }

    private void OnFOVChanged(float currentFOV)
    {
        if (currentFOV == 70)
        {
            _fovText.text = "FOV: Normal";
            _fovShadow.text = "FOV:Normal";
        }
        else if (currentFOV == 110)
        {
            _fovText.text = "FOV: Quake Pro";
            _fovShadow.text = "FOV: Quake Pro";
        }
        else
        {
            _fovText.text = $"FOV: {currentFOV}";
            _fovShadow.text = $"FOV: {currentFOV}";
        }

        _gameSettings.UpdateFOV(currentFOV);
    }
}
