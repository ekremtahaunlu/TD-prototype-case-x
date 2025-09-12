using System.Collections;
using UnityEngine;

public class EnemyFollowPath : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Transform[] waypoints;
    private int waypointIndex = 0;

    // WaveManager set edecek
    public void SetWaypoints(Transform[] points)
    {
        waypoints = points;
        waypointIndex = 0;
        if (waypoints != null && waypoints.Length > 0)
        {
            transform.position = waypoints[0].position;
        }
    }

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Transform target = waypoints[waypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            waypointIndex++;
            if (waypointIndex >= waypoints.Length)
            {
                ReachEnd();
            }
        }
    }

    void ReachEnd()
    {
        Destroy(gameObject);
    }
}
