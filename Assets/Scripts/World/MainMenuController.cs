using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject creditsPanel;

    public void OnPlayClicked()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void OnCreditsClicked()
    {
        creditsPanel.SetActive(true);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
