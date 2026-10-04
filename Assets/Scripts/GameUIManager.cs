using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance { get; private set; }

    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;

    void Awake()
    {
        Instance = this;
    }

    public void ShowLoseScreen()
    {
        losePanel.SetActive(true);
    }

    public void ShowWinScreen()
    {
        winPanel.SetActive(true);
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;

        if (PlayerStats.Instance != null)
        {
            Destroy(PlayerStats.Instance.gameObject);
        }

        SceneManager.LoadScene("MainMenu");
    }

    public void RestartWholeGame()
    {
        Time.timeScale = 1f;

        if (PlayerStats.Instance != null)
        {
            Destroy(PlayerStats.Instance.gameObject);
        }

        SceneManager.LoadScene("Level_01");
    }
}