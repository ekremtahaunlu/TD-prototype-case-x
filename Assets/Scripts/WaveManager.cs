using System.Collections;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject enemyPrefab;
    public int count = 10;
    public float spawnInterval = 0.5f;
}

public class WaveManager : MonoBehaviour
{
    public Wave[] waves;
    public Transform spawnPoint;
    public Transform[] pathWaypoints;
    public KeyCode earlyNextWaveKey = KeyCode.E;

    private int currentWave = 0;
    public int CurrentWave => currentWave;

    void Start()
    {
        StartCoroutine(WaveLoop());
    }

    IEnumerator WaveLoop()
    {
        while (currentWave < waves.Length)
        {
            Wave w = waves[currentWave];

            for (int i = 0; i < w.count; i++)
            {
                SpawnEnemy(w.enemyPrefab);
                yield return new WaitForSeconds(w.spawnInterval);
            }

            while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            {
                if (Input.GetKeyDown(earlyNextWaveKey)) break;
                yield return null;
            }

            currentWave++;
            yield return null;
        }
    }

    void SpawnEnemy(GameObject prefab)
    {
        var go = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        go.tag = "Enemy";

        var ef = go.GetComponent<EnemyFollowPath>();
        if (ef != null)
        {
            ef.waypoints = pathWaypoints;
        }
    }
}
