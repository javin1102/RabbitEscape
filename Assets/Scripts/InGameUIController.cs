using UnityEngine;
using UnityEngine.SceneManagement;
public class InGameUIController : MonoBehaviour
{
    public static InGameUIController instance;

    private InGameManager inGameManager;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject gameOverMenu;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        inGameManager = InGameManager.instance;
    }
    public void Pause()
    {
        AudioManager.instance.PlayS("button");
        inGameManager.gameState = InGameManager.GameState.PAUSE;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        AudioManager.instance.PlayS("button");
        inGameManager.gameState = InGameManager.GameState.PLAY;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    
    public void Play()
    {
        AudioManager.instance.PlayS("button");
        inGameManager.gameState = InGameManager.GameState.PLAY;
        gameOverMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {

        inGameManager.gameState = InGameManager.GameState.GAMEOVER;
        gameOverMenu.SetActive(true);
    }

    public void Quit()
    {
        AudioManager.instance.PlayS("button");
        Time.timeScale = 1;
        inGameManager.gameState = InGameManager.GameState.GAMEOVER;
        SceneManager.LoadScene(0);
    }
}
