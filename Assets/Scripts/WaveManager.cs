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
    public int enemiesPerWave = 5;

    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool spawningWave = false;

    public int CurrentWave => currentWave;
    public int EnemiesAlive => enemiesAlive;

    public static WaveManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
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
        // Artýk enemiesAlive þartý yok, E'ye basýnca dalga geliyor
        if (Input.GetKeyDown(KeyCode.E) && !spawningWave)
        {
            StopAllCoroutines();
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        spawningWave = true;
        currentWave++;

        int thisWaveEnemyCount = enemiesPerWave; // istersen dalgaya göre arttýrabilirsin

        // hâlâ sahnede düþman varsa yeni gelenleri de ekle
        enemiesAlive += thisWaveEnemyCount;

        for (int i = 0; i < thisWaveEnemyCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            EnemyFollowPath path = enemy.GetComponent<EnemyFollowPath>();
            if (path != null)
            {
                path.SetWaypoints(pathWaypoints);
                // wave’e göre hýz
                float newSpeed = 2f + (currentWave - 1) * 0.5f;
                path.speed = newSpeed;
            }

            // wave’e göre can
            SimpleHealth hp = enemy.GetComponent<SimpleHealth>();
            if (hp != null)
            {
                hp.maxHP = 5 + (currentWave - 1) * 2;
            }

            // wave’e göre renk
            Renderer rend = enemy.GetComponentInChildren<Renderer>();
            if (rend != null)
            {
                rend.material.color = GetColorForWave(currentWave);
            }

            EnemyDeathNotifier notifier = enemy.AddComponent<EnemyDeathNotifier>();
            notifier.onEnemyDestroyed = OnEnemyDestroyed;

            yield return new WaitForSeconds(timeBetweenSpawns);
        }

        spawningWave = false;
    }

    public void OnEnemyDestroyed()
    {
        enemiesAlive--;
        // otomatik sonraki dalga gelsin istiyorsan buraya logic ekle
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

    Color GetColorForWave(int wave)
    {
        switch (wave % 5)
        {
            case 1: return Color.green;
            case 2: return Color.yellow;
            case 3: return Color.red;
            case 4: return Color.magenta;
            default: return Color.white;
        }
    }
}
