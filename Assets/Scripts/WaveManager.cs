using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public Transform[] pathWaypoints;

    [Header("Wave Settings")]
    public float timeBetweenSpawns = 1f;
    public float timeBetweenWaves = 2f;
    public int enemiesPerWave = 5; // ilk dalgadaki düþman sayýsý

    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool spawningWave = false;

    // UI için property’ler
    public int CurrentWave => currentWave;
    public int EnemiesAlive => enemiesAlive;

    // Singleton istiyorsan:
    public static WaveManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Waypoints boþsa sahneden otomatik doldur
        if (pathWaypoints == null || pathWaypoints.Length == 0)
        {
            var wpParent = GameObject.Find("Waypoints");
            if (wpParent != null && wpParent.transform.childCount > 0)
            {
                int c = wpParent.transform.childCount;
                pathWaypoints = new Transform[c];
                for (int i = 0; i < c; i++)
                    pathWaypoints[i] = wpParent.transform.GetChild(i);
            }
        }

        StartCoroutine(SpawnWave());
    }

    void Update()
    {
        // Elle yeni dalga için
        if (Input.GetKeyDown(KeyCode.E) && !spawningWave && enemiesAlive <= 0)
        {
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        spawningWave = true;
        currentWave++;

        // Dalga baþýna düþman sayýsýný dalga numarasýna göre belirle
        int thisWaveEnemyCount = enemiesPerWave + (currentWave - 1) * 2;

        enemiesAlive = thisWaveEnemyCount;

        for (int i = 0; i < thisWaveEnemyCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            EnemyFollowPath path = enemy.GetComponent<EnemyFollowPath>();
            if (path != null)
                path.SetWaypoints(pathWaypoints);

            // Enemy öldüðünde WM'a bildir
            EnemyDeathNotifier notifier = enemy.AddComponent<EnemyDeathNotifier>();
            notifier.onEnemyDestroyed = OnEnemyDestroyed;

            yield return new WaitForSeconds(timeBetweenSpawns);
        }

        spawningWave = false;
    }

    public void OnEnemyDestroyed()
    {
        enemiesAlive--;
        if (enemiesAlive <= 0 && !spawningWave)
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
