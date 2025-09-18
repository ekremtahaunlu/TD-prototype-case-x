using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void RestartMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.time = 0f;
            audioSource.Play();
        }
    }
}
