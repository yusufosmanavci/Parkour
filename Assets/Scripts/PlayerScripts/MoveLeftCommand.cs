using UnityEngine;

public class MoveLeftCommand : ICommand
{
    private Transform _transform;
    private PlayerMovement _playerMovement;

    public MoveLeftCommand(Transform transform, PlayerMovement playerMovement)
    {
        _transform = transform;
        _playerMovement = playerMovement;
    }

    public void Execute()
    {
        float speed = _playerMovement.GetSpeed();
        _transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    public void Undo()
    {
        float speed = _playerMovement.GetSpeed();
        _transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
