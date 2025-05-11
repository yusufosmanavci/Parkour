using UnityEngine;

public class MoveBackwardCommand : ICommand
{
    private Transform _transform;
    private PlayerMovement _playerMovement;

    public MoveBackwardCommand(Transform transform, PlayerMovement playerMovement)
    {
        _transform = transform;
        _playerMovement = playerMovement;
    }

    public void Execute()
    {
        float speed = _playerMovement.GetSpeed();
        _transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    public void Undo()
    {
        float speed = _playerMovement.GetSpeed();
        _transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
