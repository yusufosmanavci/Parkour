using UnityEngine;

public class RunCommand : ICommand
{
    private PlayerMovement _playerMovement;

    public RunCommand(PlayerMovement movement)
    {
        _playerMovement = movement;
    }

    public void Execute()
    {
        _playerMovement.SetRun(true);
    }

    public void Undo()
    {
        _playerMovement.SetRun(false);
    }
}
