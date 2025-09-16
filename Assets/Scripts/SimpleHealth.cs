using UnityEngine;
using System;

public class SimpleHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHP = 3;
    private int hp;

    public int CurrentHP => hp;

    public Action onDeath;

    void Start()
    {
        hp = maxHP;
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;

        if (hp < 0)
            hp = 0;

        if (hp == 0)
        {
            Die();
        }
    }

    void Die()
    {
        onDeath?.Invoke();
        gameObject.SetActive(false);
    }
}
