using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsPanel;

    public void OnResumeClicked()
    {
        GameManager.instance.gameStatus = GameManager.GameStatus.Play;
        this.gameObject.SetActive(false);
    }

    public void OnSettingsClicked()
    {
        settingsPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void OnMainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
