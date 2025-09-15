using UnityEngine;

public class SimpleHealth : MonoBehaviour
{
    public int maxHP = 3;
    private int hp;

    public int CurrentHP => hp;

    void Start()
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

    void Die()
    {
        Destroy(gameObject);
    }
}
