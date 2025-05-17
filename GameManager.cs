using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    private int currentScore = 0;

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        Debug.Log("Current Score: " + currentScore);
        // Update UI here if you have one
    }
    
    // Add other game management functions as needed
}
