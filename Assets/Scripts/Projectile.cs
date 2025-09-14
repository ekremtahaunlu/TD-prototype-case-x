using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;

    private Transform target;

    public void SetTarget(Transform targetEnemy)
    {
        target = targetEnemy;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.LookAt(target);

        transform.Translate(Vector3.forward * distanceThisFrame, Space.Self);
    }

    void HitTarget()
    {
        SimpleHealth hp = target.GetComponent<SimpleHealth>();
        if (hp != null)
        {
            hp.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
