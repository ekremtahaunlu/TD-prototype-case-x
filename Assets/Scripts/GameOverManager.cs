using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;
    private bool isGameOver = false;

    public void GameOver(string reason = "Game Over")
    {
        if (isGameOver) return;
        isGameOver = true;

        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (gameOverText != null) gameOverText.text = reason;

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
