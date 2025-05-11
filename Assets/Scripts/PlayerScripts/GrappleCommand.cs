using UnityEngine;

public class GrappleCommand : ICommand
{
    private Rigidbody _rb;
    private Vector3 _targetPosition;
    private float _grappleForce;

    public GrappleCommand(Rigidbody rb, Vector3 target, float grappleForce = 20f)
    {
        _rb = rb;
        _targetPosition = target;
        _grappleForce = grappleForce;
    }

    public void Execute()
    {
        Vector3 direction = (_targetPosition - _rb.position).normalized;
        _rb.velocity = Vector3.zero;
        _rb.AddForce(direction * _grappleForce, ForceMode.Impulse);
    }

    public void Undo() { }
}
