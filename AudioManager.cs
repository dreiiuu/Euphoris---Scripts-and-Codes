using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public bool playOnStart = true;
    public bool loop = true;
    
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        if (playOnStart && backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = loop;
            audioSource.Play();
        }
    }
}
