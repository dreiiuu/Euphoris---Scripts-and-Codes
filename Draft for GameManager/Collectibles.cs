using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Settings")]
    public int pointsToAdd = 1;
    public bool destroyOnCollect = true;
    
    [Header("Effects")]
    public AudioClip collectSound;
    public GameObject collectEffect;

    private bool _isCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isCollected && other.CompareTag("Player") && !GameManager.instance.IsAllCollected())
        {
            Collect();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isCollected && other.CompareTag("Player") && !GameManager.instance.IsAllCollected())
        {
            Collect();
        }
    }

    private void Collect()
    {
        _isCollected = true;
        GameManager.instance.AddScore(pointsToAdd);
        
        if (collectSound != null)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        
        if (collectEffect != null)
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        
        if (destroyOnCollect)
            Destroy(gameObject);
        else
            DisableCollectible();
    }

    private void DisableCollectible()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        gameObject.SetActive(false);
    }

    public void ResetCollectible()
    {
        _isCollected = false;
        if (!destroyOnCollect)
        {
            GetComponent<Collider>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
            gameObject.SetActive(true);
        }
    }
}