using UnityEngine;

public class BoardGrid : MonoBehaviour
{
    [Min(2)] public int size = 8; // 8x8

    // Convert world position -> (file, rank)
    public Vector2Int WorldToSquare(Vector3 world)
    {
        // Board is a Quad: its local plane is X/Y, Z = 0
        Vector3 local = transform.InverseTransformPoint(world);
        float u = Mathf.Clamp01(local.x + 0.5f); // left→right across the board
        float v = Mathf.Clamp01(local.y + 0.5f); // bottom→top across the board

        int file = Mathf.Clamp(Mathf.FloorToInt(u * size), 0, size - 1);
        int rank = Mathf.Clamp(Mathf.FloorToInt(v * size), 0, size - 1);
        return new Vector2Int(file, rank);
    }

    // Center of a square in world space (quad plane is X/Y, Z=0)
    public Vector3 SquareCenter(Vector2Int sq, float yOverride = float.NaN)
    {
        float u = (sq.x + 0.5f) / size - 0.5f;
        float v = (sq.y + 0.5f) / size - 0.5f;

        Vector3 p = transform.TransformPoint(new Vector3(u, v, 0f));
        if (!float.IsNaN(yOverride)) p.y = yOverride;
        return p;
    }
}
