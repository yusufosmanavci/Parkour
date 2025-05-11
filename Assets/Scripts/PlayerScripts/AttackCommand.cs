using UnityEngine;

public class AttackCommand : ICommand
{
    private Animator _animator;

    public AttackCommand(Animator animator)
    {
        _animator = animator;
    }

    public void Execute()
    {
        _animator.SetTrigger("Attack");
    }

    public void Undo() { } // Genelde saldýrý geri alýnmaz
}
