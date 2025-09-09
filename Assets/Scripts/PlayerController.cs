using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float attackRange = 3f;
    public float attackCooldown = 0.5f;
    public float attackDamage = 10f;
    public LayerMask enemyLayer;

    private CharacterController cc;
    private float attackTimer = 0f;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            TryAutoAttack();
        }
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 camF = Camera.main.transform.forward;
        camF.y = 0f;
        camF.Normalize();
        Vector3 camR = Camera.main.transform.right;
        camR.y = 0f;
        camR.Normalize();

        Vector3 dir = (camR * h + camF * v).normalized;
        if (dir.sqrMagnitude > 0.01f)
        {
            // Yönlendirme (opsiyonel)
            transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * 10f);
        }

        cc.SimpleMove(dir * moveSpeed);
    }

    void TryAutoAttack()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);
        Transform nearest = null;
        float minDist = Mathf.Infinity;
        foreach (var c in hits)
        {
            float d = Vector3.Distance(transform.position, c.transform.position);
            if (d < minDist)
            {
                minDist = d;
                nearest = c.transform;
            }
        }

        if (nearest != null)
        {
            Attack(nearest);
            attackTimer = attackCooldown;
        }
    }

    void Attack(Transform target)
    {
        var health = target.GetComponent<SimpleHealth>();
        if (health != null)
        {
            health.TakeDamage(attackDamage);
        }
        // TODO: VFX / ses burada tetiklenebilir
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}