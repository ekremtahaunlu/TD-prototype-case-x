using UnityEngine;

public class EnemyFollowPath : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;  // WaveManager buradan ayarlayacak

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
        BaseHealthHandler baseHandler = FindObjectOfType<BaseHealthHandler>();
        if (baseHandler != null && baseHandler.health != null)
        {
            baseHandler.health.TakeDamage(1);
        }

        Destroy(gameObject);
    }
}
