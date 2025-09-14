using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public Transform[] pathWaypoints; // Inspector'da baðlayabilirsin

    [Header("Wave Settings")]
    public float timeBetweenSpawns = 1f;
    public int enemiesPerWave = 5;
    public float timeBetweenWaves = 2f;

    private int currentWave = 0;
    private int enemiesAlive = 0;
    public int CurrentWave => currentWave;

    void Start()
    {
        // Eðer inspector'daki pathWaypoints boþsa sahnedeki "Waypoints" objesinden al
        if (pathWaypoints == null || pathWaypoints.Length == 0)
        {
            var wpParent = GameObject.Find("Waypoints");
            if (wpParent != null && wpParent.transform.childCount > 0)
            {
                int c = wpParent.transform.childCount;
                pathWaypoints = new Transform[c];
                for (int i = 0; i < c; i++) pathWaypoints[i] = wpParent.transform.GetChild(i);
                Debug.Log($"[WaveManager] Auto-filled {c} waypoints from 'Waypoints' object.");
            }
            else
            {
                Debug.LogWarning("[WaveManager] pathWaypoints is empty and no 'Waypoints' GameObject found.");
            }
        }

        StartCoroutine(SpawnWave());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && enemiesAlive > 0)
        {
            StopAllCoroutines();
            StartCoroutine(NextWave());
        }
    }


    IEnumerator SpawnWave()
    {
        currentWave++;
        enemiesAlive = enemiesPerWave;

        for (int i = 0; i < enemiesPerWave; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            EnemyFollowPath path = enemy.GetComponent<EnemyFollowPath>();
            if (path != null)
            {
                path.SetWaypoints(pathWaypoints);
            }
            else
            {
                Debug.LogError("[WaveManager] Spawned enemy has no EnemyFollowPath component!");
            }

            EnemyDeathNotifier notifier = enemy.AddComponent<EnemyDeathNotifier>();
            notifier.onEnemyDestroyed = OnEnemyDestroyed;

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    public void OnEnemyDestroyed()
    {
        if (this == null) return;
        enemiesAlive--;
        if (enemiesAlive <= 0)
        {
            StartCoroutine(NextWave());
        }
    }

    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave());
    }
}

public class EnemyDeathNotifier : MonoBehaviour
{
    public System.Action onEnemyDestroyed;
    private void OnDestroy()
    {
        if (onEnemyDestroyed != null)
        {
            onEnemyDestroyed.Invoke();
        }
    }
}
