using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private WaveManager waveManager;
    private BaseHealth baseHealth;

    public TMP_Text waveText;
    public TMP_Text enemyCountText;
    public Slider baseHealthSlider;

    void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
        baseHealth = FindObjectOfType<BaseHealth>();
    }

    void Update()
    {
        if (waveManager != null)
        {
            waveText.text = "Wave: " + waveManager.CurrentWave;
            enemyCountText.text = "Enemies: " + waveManager.EnemiesAlive;
        }

        if (baseHealth != null && baseHealthSlider != null)
        {
            baseHealthSlider.value = (float)baseHealth.CurrentHP / baseHealth.maxHP;
        }
    }
}
