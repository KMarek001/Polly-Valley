using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private AudioListener audioListener;

    public enum GameStatus
    {
        Play,
        Pause,
        ChestOpened
    }

    public GameStatus gameStatus = GameStatus.Play;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (gameStatus == GameStatus.Pause)
            AudioController.instance.PauseSounds();

        if (gameStatus == GameStatus.Pause || gameStatus == GameStatus.ChestOpened)
            Time.timeScale = 0;

        else if(gameStatus == GameStatus.Play)
        {
            Time.timeScale = 1;
        }
    }
}
