using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else { instance = this; }
    }

    [SerializeField] private float fruitsToCollect;
    private float fruitsCollected;

    private void Start()
    {
        PauseGame();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        PauseGame();
        UIManager.Instance.ToggleGameOverMenu(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void WinGame()
    {
        PauseGame();
        UIManager.Instance.ToggleWinningMenu(true);
    }

    public void AddFruit() 
    { 
        fruitsCollected++;
        UIManager.Instance.SetFruitValue(fruitsCollected);
        if (fruitsCollected >= fruitsToCollect) { WinGame(); }
    }
}
