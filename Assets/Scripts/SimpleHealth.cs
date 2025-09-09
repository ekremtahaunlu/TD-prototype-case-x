using UnityEngine;
using System.Collections;

public class SimpleHealth : MonoBehaviour
{
    public float maxHP = 100f;
    public float hp;
    public bool invincible = false;
    public float iFrameDuration = 0.5f;

    private Renderer rend;

    void Start()
    {
        hp = maxHP;
        rend = GetComponentInChildren<Renderer>();
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