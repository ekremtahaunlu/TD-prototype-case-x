using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private WaveManager waveManager;
    private SimpleHealth baseHealth; // Base’in SimpleHealth bileşeni

    public TMP_Text waveText;
    public TMP_Text enemyCountText;
    public Slider baseHealthSlider;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;

    void Start()
    {
        // WaveManager ve BaseHealth referanslarını bul
        waveManager = WaveManager.Instance ?? FindObjectOfType<WaveManager>();
        baseHealth = FindObjectOfType<SimpleHealth>();

        // Game Over paneli kapalı başlasın
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Slider ayarlarını yap
        if (baseHealth != null && baseHealthSlider != null)
        {
            baseHealthSlider.maxValue = baseHealth.maxHP;
            baseHealthSlider.value = baseHealth.CurrentHP;
        }
    }

    void Update()
    {
        // Wave ve düşman sayısı
        if (waveManager != null)
        {
            waveText.text = $"Wave: {waveManager.CurrentWave}";
            enemyCountText.text = $"Enemies: {waveManager.EnemiesAlive}";
        }

        // Base HP slider
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
