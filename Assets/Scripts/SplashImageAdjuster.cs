using UnityEngine;
using UnityEngine.UI;

public class SplashImageAdjuster : MonoBehaviour
{
    public Image splashImage; // ������ �� ����������� ��������

    void Start()
    {
        AdjustImageToScreen();
    }

    void AdjustImageToScreen()
    {
        if (splashImage == null)
        {
            Debug.LogError("����������� �������� �� ���������!");
            return;
        }

        RectTransform rectTransform = splashImage.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.offsetMin = new Vector2(0, 0); // ������� �������
        rectTransform.offsetMax = new Vector2(0, 0); // ������� �������
    }
}
