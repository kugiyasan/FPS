using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public CharacterController controller;
    public Transform playerBody; // Should be equal to transform
    public float walkSpeed = 20f;
    public float sprintSpeed = 40f;

    [Header("Jump Settings")]
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector2 inputMovement;
    float yVelocity = 0f;
    bool isGrounded;
    bool isSprinting;
    bool isJumping;

    public void Move(InputAction.CallbackContext ctx)
    {
        inputMovement = ctx.ReadValue<Vector2>();
    }

    public void Sprint(InputAction.CallbackContext ctx)
    {
        isSprinting = true;
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        isJumping = ctx.performed;
    }

    void FixedUpdate()
    {
        // Sprint and Movement
        MoveUpdate();

        JumpUpdate();
    }

    void MoveUpdate()
    {
        // ! This statement should be called once, not every frame
        if (inputMovement == Vector2.zero)
            isSprinting = false;

        Vector3 m = inputMovement.x * playerBody.right + inputMovement.y * playerBody.forward;
        float speed = isSprinting ? sprintSpeed : walkSpeed;

        // controller.Move(m * speed * Time.deltaTime);
        controller.Move(m * speed * Time.fixedDeltaTime);
    }

    void JumpUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && yVelocity < 0)
            yVelocity = -2f;
        if (isGrounded && isJumping)
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // controller.Move(Vector3.up * yVelocity * Time.deltaTime);
        // yVelocity += gravity * Time.deltaTime;
        controller.Move(Vector3.up * yVelocity * Time.fixedDeltaTime);
        yVelocity += gravity * Time.fixedDeltaTime;
    }
}