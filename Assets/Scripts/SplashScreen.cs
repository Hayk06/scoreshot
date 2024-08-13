using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public float splashDuration = 3f; // Время показа заставки в секундах

    void Start()
    {
        Invoke("LoadMainMenu", splashDuration); // Переход к главному меню после указанного времени
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Название сцены с главным меню
    }
}
