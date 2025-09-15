using UnityEngine;

public class EnemyDeathNotifier : MonoBehaviour
{
    public System.Action onEnemyDestroyed;

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;

        if (onEnemyDestroyed != null)
            onEnemyDestroyed.Invoke();
    }
}
