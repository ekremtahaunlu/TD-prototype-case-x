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
    public int enemiesPerWave = 5;

    private int currentWave = 0;
    public int CurrentWave => currentWave;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            EnemyFollowPath path = enemy.GetComponent<EnemyFollowPath>();
            if (path != null)
            {
                path.SetWaypoints(pathWaypoints);
            }

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
}
