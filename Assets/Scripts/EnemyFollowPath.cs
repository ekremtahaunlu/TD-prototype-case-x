using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPath : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    private int currentWP = 0;

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;
        Transform target = waypoints[currentWP];
        Vector3 dir = (target.position - transform.position);
        Vector3 move = dir.normalized * speed * Time.deltaTime;
        if (move.magnitude >= dir.magnitude)
        {
            transform.position = target.position;
            currentWP++;
            if (currentWP >= waypoints.Length)
            {
                ReachEnd();
            }
        }
        else
        {
            transform.position += move;
        }
    }

    void ReachEnd()
    {
        // Burada base hasarı, puan düşürme vs yapılabilir.
        Destroy(gameObject);
    }
}
