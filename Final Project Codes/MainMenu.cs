using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() => SceneLoader.Instance.LoadScene("Select Environment");
    public void OpenControls() => SceneLoader.Instance.LoadScene("Controls");
    public void QuitGame() => SceneLoader.Instance.QuitGame();
}
