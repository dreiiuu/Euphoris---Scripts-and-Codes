using UnityEngine;

public class ControlsBack : MonoBehaviour
{
    public void BackToMenu() => SceneLoader.Instance.LoadScene("MainMenu");
}
