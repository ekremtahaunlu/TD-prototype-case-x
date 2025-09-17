using UnityEngine;
using System;

public class SimpleHealth : MonoBehaviour
{
    public int maxHP = 10;
    private int hp;

    public int CurrentHP => hp;

    public AudioClip dieClip;

    public Action onDeath;

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
        if (dieClip != null)
        {
            AudioSource.PlayClipAtPoint(dieClip, transform.position, 1.0f);
        }
        
        if (onDeath != null)
            onDeath.Invoke();

        if (CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
