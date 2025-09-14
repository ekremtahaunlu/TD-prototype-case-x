using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [Header("Wave Settings")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public Transform[] pathWaypoints;
    public float timeBetweenSpawns = 0.5f;
    public float timeBetweenWaves = 3f;
    public int enemiesPerWave = 5;

    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool spawning = false;

    public int CurrentWave => currentWave;
    public int EnemiesAlive => enemiesAlive;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryStartNextWave();
        }
    }

    public void OnEnemyDestroyed()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0 && !spawning)
        {
            StartCoroutine(NextWave());
        }
    }

    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        TryStartNextWave();
    }

    void TryStartNextWave()
    {
        if (enemiesAlive > 0 || spawning) return;

        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        spawning = true;
        currentWave++;
        enemiesAlive = enemiesPerWave;

        Debug.Log($"[WaveManager] Wave {currentWave} starting with {enemiesPerWave} enemies");

        for (int i = 0; i < enemiesPerWave; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            EnemyFollowPath path = enemy.GetComponent<EnemyFollowPath>();
            if (path != null)
                path.SetWaypoints(pathWaypoints);

            EnemyDeathNotifier notifier = enemy.AddComponent<EnemyDeathNotifier>();
            notifier.onEnemyDestroyed = OnEnemyDestroyed;

            yield return new WaitForSeconds(timeBetweenSpawns);
        }

        spawning = false;
    }
}
