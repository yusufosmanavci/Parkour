using UnityEngine;

public class MoveForwardCommand : ICommand
{
    private Transform _transform;
    private PlayerMovement _playerMovement;

    public MoveForwardCommand(Transform transform, PlayerMovement playerMovement)
    {
        _transform = transform;
        _playerMovement = playerMovement;
    }

    public void Execute()
    {
        float speed = _playerMovement.GetSpeed();
        _transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void Undo()
    {
        float speed = _playerMovement.GetSpeed();
        _transform.Translate(-Vector3.forward * speed * Time.deltaTime);
    }
}
