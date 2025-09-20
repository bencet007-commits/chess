using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class ChessPieceSnap : MonoBehaviour
{
    public BoardGrid grid;
    public float hoverLift = 0.02f;

    XRGrabInteractable grab;
    Vector2Int startSq;
    float startY;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrabbed);
        grab.selectExited.AddListener(OnReleased);
    }

    void OnDestroy()
    {
        if (grab != null)
        {
            grab.selectEntered.RemoveListener(OnGrabbed);
            grab.selectExited.RemoveListener(OnReleased);
        }
    }

    void OnGrabbed(SelectEnterEventArgs _)
    {
        startY = transform.position.y;
        startSq = grid.WorldToSquare(transform.position);
        transform.position += Vector3.up * hoverLift; // small visual lift
    }

    void OnReleased(SelectExitEventArgs _)
    {
        var targetSq = grid.WorldToSquare(transform.position);
        transform.position = grid.SquareCenter(targetSq, startY); // snap (no rules yet)
    }
}
