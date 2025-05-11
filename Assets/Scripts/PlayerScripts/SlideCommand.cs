using UnityEngine;

public class SlideCommand : ICommand
{
    private Rigidbody _rb;
    private float _slideForce;
    private Transform _transform;

    public SlideCommand(Rigidbody rb, Transform transform, float slideForce = 15f)
    {
        _rb = rb;
        _transform = transform;
        _slideForce = slideForce;
    }

    public void Execute()
    {
        Vector3 slideDirection = _transform.forward;
        _rb.AddForce(slideDirection * _slideForce, ForceMode.Impulse);
    }

    public void Undo() { }
}
