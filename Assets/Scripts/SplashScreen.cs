using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public float splashDuration = 3f; // ����� ������ �������� � ��������

    void Start()
    {
        Invoke("LoadMainMenu", splashDuration); // ������� � �������� ���� ����� ���������� �������
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // �������� ����� � ������� ����
    }
}
