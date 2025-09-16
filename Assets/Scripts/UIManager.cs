using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private WaveManager waveManager;
    private SimpleHealth baseHealth;

    public TMP_Text waveText;
    public TMP_Text enemyCountText;
    public Slider baseHealthSlider;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;

    void Start()
    {
        waveManager = WaveManager.Instance ?? FindObjectOfType<WaveManager>();
        baseHealth = FindObjectOfType<SimpleHealth>();

        if (gameOverPanel != null) 
            gameOverPanel.SetActive(false);

        if (baseHealth != null && baseHealthSlider != null)
        {
            baseHealthSlider.maxValue = baseHealth.maxHP;
            baseHealthSlider.value = baseHealth.CurrentHP;
        }
    }

    void Update()
    {
        if (waveManager != null)
        {
            waveText.text = $"Wave: {waveManager.CurrentWave}";
            enemyCountText.text = $"Enemies: {waveManager.EnemiesAlive}";
        }

        if (baseHealth != null && baseHealthSlider != null)
        {
            baseHealthSlider.value = baseHealth.CurrentHP;
        }
    }

    public void ShowGameOver(string reason = "Game Over")
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
        if (gameOverText != null)
            gameOverText.text = reason;

        Time.timeScale = 0f;
    }
}
