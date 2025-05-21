using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Score Settings")]
    private int currentScore = 0;
    private const int TARGET_COLLECTIBLES = 10;
    public Text scoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Check for ESC key only when all items are collected
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }
    }

    public void AddScore(int amount)
    {
        currentScore = Mathf.Min(currentScore + amount, TARGET_COLLECTIBLES);
        UpdateScoreUI();
        Debug.Log($"Score: {currentScore}/{TARGET_COLLECTIBLES}");
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Collectibles: {currentScore}/{TARGET_COLLECTIBLES}";
        }
    }

    public bool IsAllCollected()
    {
        return currentScore >= TARGET_COLLECTIBLES;
    }

    public void ReturnToMainMenu()
    {
        // Ensure the scene name matches EXACTLY (case-sensitive)
        SceneManager.LoadScene("MainMenu"); 
        
        // Reset the GameManager for next playthrough
        Destroy(gameObject); 
    }
}
