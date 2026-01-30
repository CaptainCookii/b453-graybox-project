using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public int w = 10;
    public int h = 10;
    public float cSize = 1f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x / cSize);
        int y = Mathf.RoundToInt(worldPos.y / cSize);
        return new Vector2Int(x, y);
    }

    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(
            gridPos.x * cSize,
            gridPos.y * cSize,
            0f
        );
    }

    public bool IsInsideGrid(Vector2Int gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.x < w &&
               gridPosition.y >= 0 && gridPosition.y < h;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                Vector3 pos = new Vector3(x * cSize, y * cSize, 0);
                Gizmos.DrawWireCube(pos, Vector3.one * cSize);
            }
        }
    }
}
