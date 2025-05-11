using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    private float currentSpeed;

    void Start()
    {
        currentSpeed = walkSpeed;
    }

    public void SetRun(bool isRunning)
    {
        currentSpeed = isRunning ? runSpeed : walkSpeed;
    }

    public float GetSpeed()
    {
        return currentSpeed;
    }
}
