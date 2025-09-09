using UnityEngine;
using System.Collections;

public class SimpleHealth : MonoBehaviour
{
    public float maxHP = 100f;
    public float hp;
    public bool invincible = false;
    public float iFrameDuration = 0.5f;
    public AudioClip deathSound;
    public ParticleSystem deathVFX;
    private AudioSource audioSource;
    private Renderer rend;

    void Start()
    {
        hp = maxHP;
        rend = GetComponentInChildren<Renderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Die()
    {
        if (deathSound != null) audioSource.PlayOneShot(deathSound);
        if (deathVFX != null) Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void TakeDamage(float amount)
    {
        if (invincible) return;
        hp -= amount;
        if (hp <= 0f)
        {
            Die();
        }
        else
        {
            StartCoroutine(IFrameFlash());
        }
    }

    IEnumerator IFrameFlash()
    {
        invincible = true;
        float elapsed = 0f;
        while (elapsed < iFrameDuration)
        {
            if (rend != null) rend.enabled = !rend.enabled;
            elapsed += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        if (rend != null) rend.enabled = true;
        invincible = false;
    }

    void Die()
    {
        // VFX / ses ekleyin
        Destroy(gameObject);
    }
}