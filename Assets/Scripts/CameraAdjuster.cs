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
        AdjustScale(goalRenderer, screenWidth, screenHeight / 2.5f); // ������������ ������ ���������������

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
    // Position the floor
    floorRenderer.transform.position = new Vector3(0, -screenHeight / 4, 0);

    // Position the wall
    wallRenderer.transform.position = new Vector3(0, screenHeight / 4, 0);

    // Position the goal and adjust it to be a little bit lower
    float goalHeight = goalRenderer.bounds.size.y / 2;
    float goalYOffset = -1.4f; // Adjust this value to move the goal down
    goalRenderer.transform.position = new Vector3(0, goalHeight + goalYOffset, 0); // Goal is positioned a bit lower
}

}
