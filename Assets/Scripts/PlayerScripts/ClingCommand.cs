using UnityEngine;

public class ClingCommand : ICommand
{
    private WallMovementController _controller;
    private Vector3 _wallNormal;

    public ClingCommand(WallMovementController controller, Vector3 wallNormal)
    {
        _controller = controller;
        _wallNormal = wallNormal;
    }

    public void Execute()
    {
        _controller.StartCling(_wallNormal);
    }

    public void Undo()
    {
        _controller.StopCling();
    }
}
