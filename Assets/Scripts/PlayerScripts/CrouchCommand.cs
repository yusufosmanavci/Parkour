using UnityEngine;

public class CrouchCommand : ICommand
{
    private Transform _playerTransform;
    private Vector3 _originalScale;
    private Vector3 _crouchScale;

    public CrouchCommand(Transform playerTransform)
    {
        _playerTransform = playerTransform;
        _originalScale = playerTransform.localScale;
        _crouchScale = new Vector3(_originalScale.x, _originalScale.y / 2f, _originalScale.z);
    }

    public void Execute()
    {
        _playerTransform.localScale = _crouchScale;
    }

    public void Undo()
    {
        _playerTransform.localScale = _originalScale;
    }
}
