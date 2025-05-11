using UnityEngine;

public class JumpCommand : ICommand
{
    private Rigidbody _rigidbody;
    private float _jumpForce;

    public JumpCommand(Rigidbody rb, float jumpForce)
    {
        _rigidbody = rb;
        _jumpForce = jumpForce;
    }

    public void Execute()
    {
        if (Mathf.Abs(_rigidbody.velocity.y) < 0.01f) // Yerde mi kontrolü
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    public void Undo() { } // Zýplamanýn geri alýnmasý mantýklý olmayabilir, boþ býrakabiliriz
}
