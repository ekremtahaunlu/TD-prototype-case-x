using UnityEngine;

public class EnemyFollowPath : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;

    private int currentWaypointIndex = 0;

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypointIndex];
        Vector3 dir = target.position - transform.position;
        transform.position += dir.normalized * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length)
            {
                ReachDestination();
            }
        }
    }

    void ReachDestination()
    {
        SimpleHealth baseHealth = FindObjectOfType<SimpleHealth>();
        if (baseHealth != null)
        {
            baseHealth.TakeDamage(1);
        }

        Destroy(gameObject);
    }
}
