using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    public SpriteRenderer backgroundRenderer;

    void Start()
    {
        AdjustCameraToFitBackground();
    }

    void AdjustCameraToFitBackground()
    {
        if (backgroundRenderer == null)
        {
            Debug.LogError("Background Renderer is not assigned!");
            return;
        }

        // ѕолучаем размеры фона
        float backgroundWidth = backgroundRenderer.bounds.size.x;
        float backgroundHeight = backgroundRenderer.bounds.size.y;

        // ѕолучаем соотношение сторон экрана
        float screenRatio = (float)Screen.width / (float)Screen.height;

        // ѕолучаем соотношение сторон фона
        float targetRatio = backgroundWidth / backgroundHeight;

        // ≈сли соотношение экрана больше соотношени€ фона, раст€гиваем камеру по высоте
        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = backgroundHeight / 2;
        }
        else
        {
            // ≈сли соотношение экрана меньше соотношени€ фона, раст€гиваем камеру по ширине
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = backgroundHeight / 2 * differenceInSize;
        }

        // “еперь раст€гиваем фон, чтобы избежать черных полос
        StretchBackgroundToFit();
    }

    void StretchBackgroundToFit()
    {
        // –аст€гиваем фон так, чтобы он занимал всю видимую область камеры
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        Vector2 scale = backgroundRenderer.transform.localScale;
        scale.x = screenWidth / backgroundRenderer.bounds.size.x;
        scale.y = screenHeight / backgroundRenderer.bounds.size.y;
        backgroundRenderer.transform.localScale = scale;
    }
}
