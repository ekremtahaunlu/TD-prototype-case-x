using UnityEngine;

public class EnemyDeathNotifier : MonoBehaviour
{
    public System.Action onEnemyDestroyed;
    private bool notified = false;

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;

        if (notified) return;
        if (onEnemyDestroyed != null)
        {
            onEnemyDestroyed.Invoke();
            notified = true;
        }
    }
}
