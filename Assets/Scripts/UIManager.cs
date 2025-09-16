using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private WaveManager waveManager;
    private SimpleHealth baseHealth; // Artık SimpleHealth

    public TMP_Text waveText;
    public TMP_Text enemyCountText;
    public Slider baseHealthSlider;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;

    void Start()
    {
        waveManager = WaveManager.Instance ?? FindObjectOfType<WaveManager>();
        baseHealth = FindObjectOfType<SimpleHealth>(); // Artık SimpleHealth

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (baseHealthSlider != null) baseHealthSlider.maxValue = 1f;
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
            baseHealthSlider.value = (float)baseHealth.CurrentHP / baseHealth.maxHP;
        }
    }

    public void ShowGameOver(string reason = "Game Over")
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (gameOverText != null) gameOverText.text = reason;
        Time.timeScale = 0f;
    }
}
