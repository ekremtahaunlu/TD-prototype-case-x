using UnityEngine;

public class BaseHealthHandler : MonoBehaviour
{
    public SimpleHealth health;
    public GameOverManager gameOverManager;

    void Start()
    {
        if (health == null)
            health = GetComponent<SimpleHealth>();

        if (health != null)
            health.onDeath += OnBaseDestroyed;
    }

    void OnBaseDestroyed()
    {
        if (gameOverManager != null)
            gameOverManager.GameOver("Game Over!");
    }
}