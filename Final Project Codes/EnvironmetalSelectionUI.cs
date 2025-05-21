using UnityEngine;

public class EnvironmentSelectionUI : MonoBehaviour
{
    void Update()
    {
        // ESC key returns to main menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }
    }

    public void LoadMountains() => LoadEnvironment("Scene 1 - Mountains");
    public void LoadForest() => LoadEnvironment("Scene 2 - Forest");
    public void LoadBeach() => LoadEnvironment("Scene 3 - Beach");
    public void LoadOpenField() => LoadEnvironment("Scene 4 - Open Field");

    private void LoadEnvironment(string sceneName)
    {
        Debug.Log($"Loading: {sceneName}");
        
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneLoader.Instance.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"Scene '{sceneName}' not found in Build Settings!");
        }
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Returning to Main Menu");
        SceneLoader.Instance.LoadScene("MainMenu");
    }
}
