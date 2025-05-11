public class WallJumpCommand : ICommand
{
    private WallMovementController _controller;

    public WallJumpCommand(WallMovementController controller)
    {
        _controller = controller;
    }

    public void Execute()
    {
        _controller.WallJump();
    }

    public void Undo() { } // Wall jump undo edilmez
}
