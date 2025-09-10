using UnityEngine;

public class SimpleHealth : MonoBehaviour
{
    public int maxHP;
    public int hp { get; private set; }

    private void Start()
    {
        hp = maxHP;
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        if (tag == "Base")
        {
            Debug.Log("Base destroyed! Game Over!");
        }
    }
}
