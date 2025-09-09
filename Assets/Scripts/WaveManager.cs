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
    public KeyCode earlyNextWaveKey = KeyCode.E; // ekstra: erken çağırma tuşu

    private int currentWave = 0;
    private bool running = false;
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
            running = true;
            for (int i = 0; i < w.count; i++)
            {
                SpawnEnemy(w.enemyPrefab);
                yield return new WaitForSeconds(w.spawnInterval);
            }

            // Dalga spawn'ı bitti. Sonraki dalgayı bekle (düşman kalmayana kadar) veya erken tuşuna basılmasını bekle.
            while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            {
                if (Input.GetKeyDown(earlyNextWaveKey)) break;
                yield return null;
            }

            currentWave++;
            running = false;
            yield return null;
        }

        // Tüm dalgalar bitti — burada win condition ekleyebilirsiniz.
    }

    void SpawnEnemy(GameObject prefab)
    {
        var go = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        var ef = go.GetComponent<EnemyFollowPath>();
        if (ef != null) ef.waypoints = pathWaypoints;
        // Enemy prefab'ına Tag = "Enemy" atamayı unutmayın
    }
}