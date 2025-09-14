using UnityEngine;

public class EnemyFollowPath : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Transform[] waypoints;
    private int waypointIndex = 0;
    private Transform target;

    void Start()
    {
        if ((waypoints == null || waypoints.Length == 0))
        {
            var wpParent = GameObject.Find("Waypoints");
            if (wpParent != null && wpParent.transform.childCount > 0)
            {
                int c = wpParent.transform.childCount;
                waypoints = new Transform[c];
                for (int i = 0; i < c; i++) waypoints[i] = wpParent.transform.GetChild(i);
                waypointIndex = 0;
                target = waypoints[0];
                Debug.Log($"[EnemyFollowPath] Fallback: found {c} waypoints for {name}");
            }
        }
        else
        {
            if (waypoints.Length > 0)
            {
                waypointIndex = 0;
                target = waypoints[0];
            }
        }

        var rb = GetComponent<Rigidbody>();
        if (rb != null && !rb.isKinematic)
        {
            rb.isKinematic = true;
            Debug.LogWarning($"[EnemyFollowPath] Rigidbody on {name} set to isKinematic=true to allow transform movement.");
        }
    }

    public void SetWaypoints(Transform[] points)
    {
        if (points == null || points.Length == 0)
        {
            Debug.LogWarning("[EnemyFollowPath] SetWaypoints called with empty points.");
            return;
        }

        waypoints = points;
        waypointIndex = 0;
        target = waypoints[0];

        transform.position = waypoints[0].position;

        Debug.Log($"[EnemyFollowPath] Waypoints set ({waypoints.Length}) on {name}. first: {target.position}");
    }

    void Update()
    {
        if (target == null) return;

        Vector3 newPos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.position = newPos;

        if (Terrain.activeTerrain != null)
        {
            Vector3 pos = transform.position;
            float h = Terrain.activeTerrain.SampleHeight(pos) + Terrain.activeTerrain.GetPosition().y;
            pos.y = h;
            transform.position = pos;
        }

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
            Debug.Log($"[EnemyFollowPath] {name} reached base and dealt 1 damage. BaseHP now: {baseHealth.CurrentHP}/{baseHealth.maxHP}");
        }

        WaveManager wm = FindObjectOfType<WaveManager>();
        if (wm != null) wm.OnEnemyDestroyed();

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        if (waypoints == null) return;
        Gizmos.color = Color.yellow;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] != null)
            {
                Gizmos.DrawSphere(waypoints[i].position, 0.15f);
                if (i + 1 < waypoints.Length && waypoints[i + 1] != null)
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }
        if (target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(target.position, 0.2f);
        }
    }
}
