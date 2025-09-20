using UnityEngine;

[ExecuteAlways]
public class ChessBoardBuilder : MonoBehaviour
{
    public BoardGrid grid;                 
    public Material lightSquare;
    public Material darkSquare;
    public float squareLift = 0.001f; // small lift above parent to avoid z-fighting

    [ContextMenu("Rebuild Board")]
    public void RebuildBoard()
    {
        // Hide the parent quad if it exists (avoids z-fighting)
        var parentMR = GetComponent<MeshRenderer>();
        if (parentMR) parentMR.enabled = false;

        // Clear old tiles
        for (int i = transform.childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);

        // Build 8x8
        for (int r = 0; r < 8; r++)
        for (int f = 0; f < 8; f++)
        {
            var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            quad.name = $"sq_{f}_{r}";
            quad.transform.SetParent(transform, false);

            // IMPORTANT: keep local rotation = identity so it inherits the parent's 90°,
            // not an extra 90° that flips it away
            quad.transform.localRotation = Quaternion.identity;
            quad.transform.localScale = new Vector3(1f / 8f, 1f / 8f, 1f);

            // Place at the center of the square (slightly lifted)
            var p = grid.SquareCenter(new Vector2Int(f, r));
            p.y += squareLift;
            quad.transform.position = p;

            var mr = quad.GetComponent<MeshRenderer>();
            mr.sharedMaterial = ((f + r) % 2 == 0) ? lightSquare : darkSquare;

            DestroyImmediate(quad.GetComponent<Collider>());
        }
    }

    void OnEnable() { if (!grid) grid = GetComponent<BoardGrid>(); }
}
