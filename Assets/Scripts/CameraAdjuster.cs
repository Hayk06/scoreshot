using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    public SpriteRenderer wallRenderer;
    public SpriteRenderer floorRenderer;
    public SpriteRenderer goalRenderer;

    void Start()
    {
        AdjustElementsToScreenSize();
    }

    void AdjustElementsToScreenSize()
    {
        if (wallRenderer == null || floorRenderer == null || goalRenderer == null)
        {
            Debug.LogError("��������� �����, ���� ��� ����� �� ���������!");
            return;
        }

        // �������� ������� ������
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        // ����������� ������� ��� �����, ���� � �����
        AdjustScale(floorRenderer, screenWidth, screenHeight / 2); // ��� ������ �������� ������ �������� ������
        AdjustScale(wallRenderer, screenWidth, screenHeight / 2);  // ����� ������ �������� ������� �������� ������
        AdjustScale(goalRenderer, screenWidth / 3, screenHeight / 4); // ������������ ������ ���������������

        // ������������� �������� �� ������
        PositionElements(screenHeight);
    }

    void AdjustScale(SpriteRenderer renderer, float targetWidth, float targetHeight)
    {
        Vector2 scale = renderer.transform.localScale;
        scale.x = targetWidth / renderer.bounds.size.x;
        scale.y = targetHeight / renderer.bounds.size.y;
        renderer.transform.localScale = scale;
    }

    void PositionElements(float screenHeight)
    {
        // ���������������� ����
        floorRenderer.transform.position = new Vector3(0, -screenHeight / 4, 0);

        // ���������������� �����
        wallRenderer.transform.position = new Vector3(0, screenHeight / 4, 0);

        // ���������������� �����
        float goalHeight = goalRenderer.bounds.size.y / 2;
        goalRenderer.transform.position = new Vector3(0, goalHeight, 0); // ������ ��������������� �� ������ ������ �������
    }
}
