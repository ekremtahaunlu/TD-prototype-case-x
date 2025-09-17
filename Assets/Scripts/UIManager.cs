using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private WaveManager waveManager;
    private SimpleHealth baseHealth;
    private BaseHealthHandler baseHealthHandler;

    public TMP_Text waveText;
    public TMP_Text enemyCountText;
    public Slider baseHealthSlider;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;

    void Start()
    {
        waveManager = WaveManager.Instance ?? FindObjectOfType<WaveManager>();
        
        baseHealthHandler = FindObjectOfType<BaseHealthHandler>();
        if (baseHealthHandler != null && baseHealthHandler.health != null)
        {
            baseHealth = baseHealthHandler.health;
        }
        else
        {
            baseHealth = FindObjectOfType<SimpleHealth>();
        }

        if (gameOverPanel != null) 
            gameOverPanel.SetActive(false);

        SetupHealthSlider();
    }

    void SetupHealthSlider()
    {
        if (baseHealth != null && baseHealthSlider != null)
        {
            baseHealthSlider.maxValue = baseHealth.maxHP;
            baseHealthSlider.value = baseHealth.CurrentHP;
        }
        else
        {
            if (baseHealth == null) Debug.LogWarning("BaseHealth null!");
            if (baseHealthSlider == null) Debug.LogWarning("BaseHealthSlider null!");
        }
    }

    void Update()
    {
        UpdateWaveInfo();
        UpdateHealthSlider();
    }

    void UpdateWaveInfo()
    {
        if (waveManager != null)
        {
            if (waveText != null)
                waveText.text = $"Wave: {waveManager.CurrentWave}";
            if (enemyCountText != null)
                enemyCountText.text = $"Enemies: {waveManager.EnemiesAlive}";
        }
    }

    void UpdateHealthSlider()
    {
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