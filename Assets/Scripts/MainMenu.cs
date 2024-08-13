using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Название сцены с игрой
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
