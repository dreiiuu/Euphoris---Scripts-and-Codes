using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    private int currentScore = 0;
    private const int TARGET_COLLECTIBLES = 10; // Exact target count

    private void Awake()
    {
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
        // Clamp the score to never exceed target
        currentScore = Mathf.Min(currentScore + amount, TARGET_COLLECTIBLES);
        Debug.Log($"Score: {currentScore}/{TARGET_COLLECTIBLES}");
    }

    public bool IsAllCollected()
    {
        return currentScore >= TARGET_COLLECTIBLES;
    }
}
