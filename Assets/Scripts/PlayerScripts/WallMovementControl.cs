using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WallMovementController : MonoBehaviour
{
    public float wallSlideSpeed = 2f;
    public float wallJumpForce = 7f;
    public float wallJumpPushForce = 5f;

    private Rigidbody rb;
    private bool isClinging;
    private Vector3 lastWallNormal;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void StartCling(Vector3 wallNormal)
    {
        if (isClinging) return;

        isClinging = true;
        rb.useGravity = false;
        rb.velocity = new Vector3(0, -wallSlideSpeed, 0);
        lastWallNormal = wallNormal;
    }

    public void StopCling()
    {
        if (!isClinging) return;

        rb.useGravity = true;
        isClinging = false;
    }

    public void WallJump()
    {
        if (!isClinging) return;

        StopCling();
        Vector3 jumpDir = (Vector3.up * wallJumpForce) + (lastWallNormal * wallJumpPushForce);
        rb.velocity = Vector3.zero;
        rb.AddForce(jumpDir, ForceMode.Impulse);
    }

    public bool IsClinging() => isClinging;
}
