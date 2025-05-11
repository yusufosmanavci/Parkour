using UnityEngine;

public class WallInputHandler : MonoBehaviour
{
    public LayerMask wallLayer;
    public float wallCheckDistance = 0.6f;

    private WallMovementController _controller;

    void Start()
    {
        _controller = GetComponent<WallMovementController>();
    }

    void Update()
    {
        if (IsTouchingWall(out RaycastHit hit) && !IsGrounded() && Input.GetKey(KeyCode.W))
        {
            if (!_controller.IsClinging())
            {
                ICommand clingCommand = new ClingCommand(_controller, hit.normal);
                clingCommand.Execute();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ICommand wallJumpCommand = new WallJumpCommand(_controller);
                wallJumpCommand.Execute();
            }
        }
        else if (_controller.IsClinging())
        {
            ICommand stopClingCommand = new ClingCommand(_controller, Vector3.zero);
            stopClingCommand.Undo();
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private bool IsTouchingWall(out RaycastHit wallHit)
    {
        Vector3[] directions = { transform.right, -transform.right };
        foreach (var dir in directions)
        {
            if (Physics.Raycast(transform.position, dir, out wallHit, wallCheckDistance, wallLayer))
                return true;
        }
        wallHit = default;
        return false;
    }
}
