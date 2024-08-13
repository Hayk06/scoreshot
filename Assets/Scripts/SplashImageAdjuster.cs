using UnityEngine;
using UnityEngine.UI;

public class SplashImageAdjuster : MonoBehaviour
{
    public Image splashImage; // Ссылка на изображение заставки

    void Start()
    {
        AdjustImageToScreen();
    }

    void AdjustImageToScreen()
    {
        if (splashImage == null)
        {
            Debug.LogError("Изображение заставки не назначено!");
            return;
        }

        RectTransform rectTransform = splashImage.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.offsetMin = new Vector2(0, 0); // Убираем отступы
        rectTransform.offsetMax = new Vector2(0, 0); // Убираем отступы
    }
}
