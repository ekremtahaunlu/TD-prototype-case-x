using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private WaveManager waveManager;
    private SimpleHealth baseHealth;

    public TMP_Text waveText;
    public TMP_Text enemyCountText;
    public Slider baseHealthSlider;

    void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
        baseHealth = FindObjectOfType<SimpleHealth>();

        if (baseHealthSlider != null && baseHealth != null)
        {
            baseHealthSlider.maxValue = baseHealth.maxHP;
            baseHealthSlider.value = baseHealth.CurrentHP;
        }
    }

    void Update()
    {
        if (waveManager != null && waveText != null && enemyCountText != null)
        {
            waveText.text = "Wave: " + (waveManager.CurrentWave + 1);
            enemyCountText.text = "Enemies: " + GameObject.FindGameObjectsWithTag("Enemy").Length;
        }

        if (baseHealth != null && baseHealthSlider != null)
        {
            baseHealthSlider.value = baseHealth.CurrentHP;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
