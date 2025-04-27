using NUnit.Framework;
using System.Collections.Generic;
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

    [SerializeField] private List<Transform> fruitsList;
    private float fruitsToCollect;
    private float fruitsCollected;
    public bool playing;

    private void Start()
    {
        PauseGame();
        fruitsToCollect = fruitsList.Count;
        UIManager.Instance.SetFruitValue(0);
    }

    private void Update()
    {
        if (playing && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
            UIManager.Instance.TogglePauseMenu(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        playing = true;
    }

    public void PauseGame()
    {
        playing = false;
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

    public float GetWinCondition() { return fruitsToCollect; }
}
