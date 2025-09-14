using UnityEngine;

public class EnemyFollowPath : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Transform[] waypoints;
    private int waypointIndex = 0;
    private Transform target;

    public void SetWaypoints(Transform[] points)
    {
        if (points == null || points.Length == 0) return;

        waypoints = points;
        waypointIndex = 0;
        target = waypoints[0];
        transform.position = waypoints[0].position;
    }

    void Update()
    {
        if (target == null) return;

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.15f)
        {
            waypointIndex++;
            if (waypointIndex >= waypoints.Length)
            {
                ReachEnd();
                return;
            }
            target = waypoints[waypointIndex];
        }
    }

    void ReachEnd()
    {
        BaseHealth baseHealth = FindObjectOfType<BaseHealth>();
        if (baseHealth != null)
        {
            baseHealth.TakeDamage(1);
        }

        if (WaveManager.Instance != null)
            WaveManager.Instance.OnEnemyDestroyed();

        Destroy(gameObject);
    }
}
