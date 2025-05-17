using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Settings")]
    public int pointsToAdd = 1;
    public bool destroyOnCollect = true;
    
    [Header("Effects")]
    public AudioClip collectSound;
    public GameObject collectEffect; // Assign your particle effect prefab here

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void Collect()
    {
        // Handle scoring
        if (GameManager.instance != null)
        {
            GameManager.instance.AddScore(pointsToAdd);
        }
        
        // Play sound if assigned
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }
        
        // Show particle effect if assigned
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }
        
        // Remove the collectible
        if (destroyOnCollect)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
