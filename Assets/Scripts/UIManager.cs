using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private WaveManager waveManager;
    private SimpleHealth playerHealth;

    public TMP_Text waveText;
    public TMP_Text enemyCountText;
    public Slider playerHealthSlider;

    void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
        playerHealth = FindObjectOfType<PlayerController>().GetComponent<SimpleHealth>();

        if (playerHealthSlider != null && playerHealth != null)
        {
            playerHealthSlider.maxValue = playerHealth.maxHealth;
            playerHealthSlider.value = playerHealth.maxHealth;
        }
    }

    void Update()
    {
        if (waveManager != null && waveText != null && enemyCountText != null)
        {
            waveText.text = "Wave: " + (waveManager.CurrentWave + 1);
            enemyCountText.text = "Enemies: " + GameObject.FindGameObjectsWithTag("Enemy").Length;
        }

        if (playerHealth != null && playerHealthSlider != null)
        {
            playerHealthSlider.value = playerHealth.CurrentHealth; // 👈 currentHealth'i public property yaptık
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
