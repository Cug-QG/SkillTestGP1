using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
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

    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject winningMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] TextMeshProUGUI fruitsValue;
    [SerializeField] List<Image> lifes;

    public void ToggleGameOverMenu(bool isActive) => gameOverMenu.SetActive(isActive);
    public void ToggleWinningMenu(bool isActive) => winningMenu.SetActive(isActive);
    public void TogglePauseMenu(bool isActive) => pauseMenu.SetActive(isActive);
    public void SetFruitValue(float value) { fruitsValue.text = value.ToString() + "/" + GameManager.Instance.GetWinCondition(); }
    public void SetHP(float value) 
    {
        for (int i = 0; i < lifes.Count; i++)
        {
            if (i < value) { lifes[i].gameObject.SetActive(true); }
            else { lifes[i].gameObject.SetActive(false); }
        }
    }
}
