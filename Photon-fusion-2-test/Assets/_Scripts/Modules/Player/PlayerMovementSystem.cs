using Fusion;
using UnityEngine;

public class PlayerMovementSystem : NetworkBehaviour
{
    #region Component Configs

    [SerializeField] private float normalSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float smoothTime;
    [SerializeField] private float jumpForce;
    [SerializeField] private float accelerate;

    private CharacterController controller;
    private Vector3 moveDirection;
    private float turnSmoothVelocity;
    private Vector3 velocity;
    private float gravityValue = -9.81f;
    private bool isJump;
    private bool isSprint;
    private float currentSpeed;

    #endregion

    public void Init()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = normalSpeed;
    }

    private void Rotate()
    {
        if (moveDirection == Vector3.zero)
        {
            return;
        }
        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    private void Jumping()
    {
        if (controller.isGrounded)
        {
            velocity = new Vector3(0, -1, 0);
        }
        velocity.y += gravityValue * Runner.DeltaTime;
        if (isJump && controller.isGrounded)
        {
            velocity.y += jumpForce;
        }
    }

    public void Move(Vector2 direction)
    {
        moveDirection = new Vector3(direction.x, 0, direction.y);
    }

    public void Jump()
    {
        if (controller.isGrounded)
        {
            isJump = true;
        }
    }

    public void Sprint()
    {
        isSprint = !isSprint;
    }

    public void Tick()
    {
        currentSpeed = isSprint
            ? Mathf.SmoothStep(currentSpeed, sprintSpeed, accelerate * Runner.DeltaTime)
            : Mathf.SmoothStep(currentSpeed, normalSpeed, accelerate * Runner.DeltaTime);
        Jumping();
        Rotate();
        controller.Move(velocity + moveDirection * currentSpeed * Runner.DeltaTime);
        isJump = false;
    }
}