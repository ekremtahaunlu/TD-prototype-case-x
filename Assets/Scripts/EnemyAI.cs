using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    public float speed = 2f;
    public float attackRange = 1.5f;
    public int damage = 1;
    public float attackCooldown = 1f;

    private float lastAttackTime;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            // Oyuncuya doðru ilerle
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            transform.LookAt(player);
        }
        else
        {
            // Saldýrý
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                SimpleHealth playerHealth = player.GetComponent<SimpleHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
                lastAttackTime = Time.time;
            }
        }
    }
}
