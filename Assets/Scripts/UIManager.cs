using TMPro;
using UnityEngine;

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
    [SerializeField] TextMeshProUGUI fruitsValue;

    public void ToggleGameOverMenu(bool isActive) => gameOverMenu.SetActive(isActive);
    public void ToggleWinningMenu(bool isActive) => winningMenu.SetActive(isActive);
    public void SetFruitValue(float value) { fruitsValue.text = value.ToString(); }
}
