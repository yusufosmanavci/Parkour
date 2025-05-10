using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AdvancedFirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5.0f;
    public float sprintSpeed = 8.0f;
    public float crouchSpeed = 2.5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Mouse Look Settings")]
    public Transform playerCamera;
    public float mouseSensitivity = 2.0f;
    public float verticalLookLimit = 90.0f;

    [Header("Crouch Settings")]
    public float crouchHeight = 1.0f;
    public float standingHeight = 2.0f;
    public float crouchTransitionSpeed = 8.0f;

    [Header("Slide Settings")]
    public float slideDuration = 0.8f;
    public float slideSpeed = 10f;
    public AnimationCurve slideSpeedOverTime;

    private CharacterController characterController;
    private Vector3 moveDirection;
    private float verticalVelocity;
    private float xRotation = 0f;

    private bool isSprinting = false;
    private bool isCrouching = false;
    private bool isSliding = false;
    private float slideTimer = 0f;
    private Vector3 slideDirection;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();

        if (isSliding)
        {
            HandleSlideMovement();
        }
        else
        {
            HandleMovement();
            HandleJump();
            HandleCrouch();
            CheckSlideStart();
            HandleSprintToggle();
        }
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalLookLimit, verticalLookLimit);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        float currentSpeed = walkSpeed;
        if (isSprinting && !isCrouching)
        {
            currentSpeed = sprintSpeed;
        }
        else if (isCrouching)
        {
            currentSpeed = crouchSpeed;
        }


        moveDirection = move * currentSpeed;

        if (characterController.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        verticalVelocity += gravity * Time.deltaTime;
        moveDirection.y = verticalVelocity;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && characterController.isGrounded && !isCrouching && !isSliding)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isSprinting)
        {
            isCrouching = !isCrouching;
        }


        float targetHeight = isCrouching ? crouchHeight : standingHeight;
        characterController.height = Mathf.Lerp(characterController.height, targetHeight, Time.deltaTime * crouchTransitionSpeed);

        Vector3 camPos = playerCamera.localPosition;
        camPos.y = Mathf.Lerp(camPos.y, isCrouching ? crouchHeight / 2 : standingHeight / 2, Time.deltaTime * crouchTransitionSpeed);
        playerCamera.localPosition = camPos;
    }

    void CheckSlideStart()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) &&
            isSprinting &&
            characterController.isGrounded &&
            characterController.velocity.magnitude > 1f)
        {
            StartSlide();
        }
    }


    void StartSlide()
    {
        isSliding = true;
        slideTimer = slideDuration;
        slideDirection = transform.forward;
        isCrouching = true; // Force crouch during slide
    }


    void HandleSlideMovement()
    {
        slideTimer -= Time.deltaTime;
        float slideFactor = slideSpeedOverTime.Evaluate(1 - (slideTimer / slideDuration));
        Vector3 slideMove = slideDirection * slideSpeed * slideFactor;
        slideMove.y = verticalVelocity;
        verticalVelocity += gravity * Time.deltaTime;

        characterController.Move(slideMove * Time.deltaTime);

        // End slide
        if (slideTimer <= 0f)
        {
            isSliding = false;
            isCrouching = false; // Return to standing after slide
        }

    }

    void HandleSprintToggle()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = !isSprinting;
        }

        // Prevent sprint while crouching
        if (isCrouching && isSprinting)
        {
            isSprinting = false;
        }
    }

}
