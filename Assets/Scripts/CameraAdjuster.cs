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

        // �������� ������� ����
        float backgroundWidth = backgroundRenderer.bounds.size.x;
        float backgroundHeight = backgroundRenderer.bounds.size.y;

        // �������� ����������� ������ ������
        float screenRatio = (float)Screen.width / (float)Screen.height;

        // �������� ����������� ������ ����
        float targetRatio = backgroundWidth / backgroundHeight;

        // ���� ����������� ������ ������ ����������� ����, ����������� ������ �� ������
        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = backgroundHeight / 2;
        }
        else
        {
            // ���� ����������� ������ ������ ����������� ����, ����������� ������ �� ������
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = backgroundHeight / 2 * differenceInSize;
        }

        // ������ ����������� ���, ����� �������� ������ �����
        StretchBackgroundToFit();
    }

    void StretchBackgroundToFit()
    {
        // ����������� ��� ���, ����� �� ������� ��� ������� ������� ������
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        Vector2 scale = backgroundRenderer.transform.localScale;
        scale.x = screenWidth / backgroundRenderer.bounds.size.x;
        scale.y = screenHeight / backgroundRenderer.bounds.size.y;
        backgroundRenderer.transform.localScale = scale;
    }
}
