using UnityEngine;

public class SimpleHealth : MonoBehaviour
{
    public int maxHP = 20;
    public int hp;

    void Start()
    {
        hp = maxHP;
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            hp = 0;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Base destroyed! Game Over!");
    }
}
