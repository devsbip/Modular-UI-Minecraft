using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [Header("Menus References")]
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _optionsMenu;

    public void OnPlayClicked()
    {
        Debug.Log("Game Starting...");
    }

    public void OnOptionsClicked()
    {
        _mainMenu.SetActive(false);
        _optionsMenu.SetActive(true);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
        Debug.Log("Game Closing...");
    }
}
