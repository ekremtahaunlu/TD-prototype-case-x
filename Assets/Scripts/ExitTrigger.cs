using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public SimpleHealth baseHealth;
    public int damagePerEnemy = 1;

    void Start()
    {
        if (baseHealth == null)
        {
            BaseHealthHandler baseHandler = FindObjectOfType<BaseHealthHandler>();
            if (baseHandler != null) baseHealth = baseHandler.health;
            if (baseHealth == null) baseHealth = FindObjectOfType<SimpleHealth>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (baseHealth != null)
                baseHealth.TakeDamage(damagePerEnemy);

            Destroy(other.gameObject);
        }
    }
}
