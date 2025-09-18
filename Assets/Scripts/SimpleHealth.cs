using UnityEngine;
using System;

public class SimpleHealth : MonoBehaviour
{
    public int maxHP = 10;
    private int hp;

    public int CurrentHP => hp;
    public Action onDeath;

    [Header("Audio")]
    public AudioClip dieClip;
    public AudioSource audioSource;

    void Start()
    {
        hp = maxHP;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
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
        if (onDeath != null)
            onDeath.Invoke();

        if (CompareTag("Enemy"))
        {
            if (dieClip != null)
            {
                if (audioSource != null)
                {
                    audioSource.PlayOneShot(dieClip);
                }
                else
                {
                    AudioSource.PlayClipAtPoint(dieClip, transform.position);
                }
            }

            Destroy(gameObject, 0.1f);
        }
    }
}
