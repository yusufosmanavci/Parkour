using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private ICommand moveForward;
    private ICommand moveBackward;
    private ICommand moveLeft;
    private ICommand moveRight;
    private ICommand jumpCommand;
    private ICommand runCommand;
    private ICommand attackCommand;

    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        moveForward = new MoveForwardCommand(transform, playerMovement);
        moveBackward = new MoveBackwardCommand(transform, playerMovement);
        moveLeft = new MoveLeftCommand(transform, playerMovement);
        moveRight = new MoveRightCommand(transform, playerMovement);

        jumpCommand = new JumpCommand(GetComponent<Rigidbody>(), jumpForce);
        runCommand = new RunCommand(playerMovement);
        attackCommand = new AttackCommand(GetComponent<Animator>());
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) moveForward.Execute();
        if (Input.GetKey(KeyCode.S)) moveBackward.Execute();
        if (Input.GetKey(KeyCode.A)) moveLeft.Execute();
        if (Input.GetKey(KeyCode.D)) moveRight.Execute();

        if (Input.GetKeyDown(KeyCode.Space)) jumpCommand.Execute();
        if (Input.GetKeyDown(KeyCode.Mouse0)) attackCommand.Execute(); // Sol týk saldýrý

        if (Input.GetKeyDown(KeyCode.LeftShift)) runCommand.Execute();
        if (Input.GetKeyUp(KeyCode.LeftShift)) runCommand.Undo();
    }
}
