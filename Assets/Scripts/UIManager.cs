using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private WaveManager waveManager;
    private SimpleHealth playerHealth;
    public Text waveText;
    public Text enemyCountText;
    public Slider playerHealthSlider;
    void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
        playerHealth = FindObjectOfType<PlayerController>().GetComponent<SimpleHealth>();
    }

    void Update()
    {
        if (waveManager != null)
        {
            waveText.text = "Wave: " + (waveManager.CurrentWave + 1);
            enemyCountText.text = "Enemies: " + GameObject.FindGameObjectsWithTag("Enemy").Length;
        }

        if (playerHealth != null)
        {
            playerHealthSlider.value = playerHealth.hp / playerHealth.maxHP;
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
