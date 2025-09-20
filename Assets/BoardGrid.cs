using UnityEngine;

public class BoardGrid : MonoBehaviour
{
    [Min(2)]
    public int size = 8; // 8x8 grid

    // Convert a world position to board square indices (file, rank) in 0..size-1
    public Vector2Int WorldToSquare(Vector3 world)
    {
        // Assumes the board is a 1x1 quad on the XZ plane, centered at local (0,0,0)
        Vector3 local = transform.InverseTransformPoint(world);
        float u = Mathf.Clamp01(local.x + 0.5f);
        float v = Mathf.Clamp01(local.z + 0.5f);

        int file = Mathf.Clamp(Mathf.FloorToInt(u * size), 0, size - 1);
        int rank = Mathf.Clamp(Mathf.FloorToInt(v * size), 0, size - 1);
        return new Vector2Int(file, rank);
    }

    // Get the world-space center of a square; optionally keep a specific Y height
    public Vector3 SquareCenter(Vector2Int sq, float yOverride = float.NaN)
    {
        float u = (sq.x + 0.5f) / size - 0.5f;
        float v = (sq.y + 0.5f) / size - 0.5f;

        Vector3 p = transform.TransformPoint(new Vector3(u, 0f, v));
        if (!float.IsNaN(yOverride)) p.y = yOverride;
        return p;
    }
}

