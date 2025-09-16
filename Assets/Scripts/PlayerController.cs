using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Combat")]
    public float attackRange = 2f;
    public float attackRate = 1f;
    public int attackDamage = 1;

    [Header("References")]
    public GameOverManager gameOverManager;
    public SimpleHealth health;

    private float attackCooldown = 0f;

    void Start()
    {
        if (health == null)
            health = GetComponent<SimpleHealth>();

        if (health != null)
            health.onDeath += OnDeath;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(h, 0f, v).normalized;
        transform.position += move * moveSpeed * Time.deltaTime;

        if (move != Vector3.zero)
            transform.forward = move;

        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0f)
        {
            GameObject enemy = GetNearestEnemy();
            if (enemy != null)
            {
                Attack(enemy);
                attackCooldown = 1f / attackRate;
            }
        }
    }

    GameObject GetNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float shortestDist = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < shortestDist && dist <= attackRange)
            {
                shortestDist = dist;
                nearest = enemy;
            }
        }
        return nearest;
    }

    void Attack(GameObject enemy)
    {
        SimpleHealth enemyHealth = enemy.GetComponent<SimpleHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(attackDamage);
        }
    }

    void OnDeath()
    {
        if (gameOverManager != null)
            gameOverManager.GameOver("Player öldü");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
