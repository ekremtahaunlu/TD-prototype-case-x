using UnityEngine;

public class EnemyDeathNotifier : MonoBehaviour
{
    public System.Action onEnemyDestroyed;

    void OnDestroy()
    {
        if (onEnemyDestroyed != null)
            onEnemyDestroyed.Invoke();
    }
}