using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
  [Header("Character Movement")]
  public CharacterController controller;
  public Transform playerBody; // Should be equal to transform
  public float walkSpeed = 20f;
  public float sprintSpeed = 40f;

  [Header("Character Look Around")]
  public Transform camTransform;
  public float lookSensitivity = 20f;

  [Header("Jump Settings")]
  public float gravity = -9.81f;
  public float jumpHeight = 2f;
  public Transform groundCheck;
  public float groundDistance = 0.4f;
  public LayerMask groundMask;

  PlayerControls controls;
  Vector2 inputMovement;
  Vector2 inputRotation;
  float yVelocity = 0f;
  float xRotation = 0f;
  bool isGrounded;
  bool isSprinting;
  bool isJumping;
  // bool isShooting;

  void Awake()
  {
    controls = new PlayerControls();

    // controls.Gameplay.Move.performed += ctx => inputMovement = ctx.ReadValue<Vector2>();
    // controls.Gameplay.Move.canceled += ctx => inputMovement = Vector2.zero;

    controls.Gameplay.Look.performed += ctx => inputRotation = ctx.ReadValue<Vector2>();
    controls.Gameplay.Look.canceled += ctx => inputRotation = Vector2.zero;

    controls.Gameplay.Sprint.performed += ctx => isSprinting = true;
    controls.Gameplay.Sprint.canceled += ctx => isSprinting = false;

    controls.Gameplay.Jump.performed += ctx => isJumping = true;
    controls.Gameplay.Jump.canceled += ctx => isJumping = false;

    // controls.Gameplay.Fire.performed += ctx => isShooting = true;
    // controls.Gameplay.Fire.canceled += ctx => isShooting = false;
  }

  void Start()
  {
    Cursor.lockState = CursorLockMode.Locked;
  }

  void Update()
  {
    // Move();
    Rotate();
    Jump();
  }

  void Move()
  {
    // ! This statement should be called once, not every frame
    if (inputMovement == Vector2.zero)
      isSprinting = false;

    Vector3 m = inputMovement.x * playerBody.right + inputMovement.y * playerBody.forward;
    float speed = isSprinting ? sprintSpeed : walkSpeed;

    controller.Move(m * speed * Time.deltaTime);
  }

  void Jump()
  {
    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    if (isGrounded && yVelocity < 0)
      yVelocity = -2f;
    if (isGrounded && isJumping)
      yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

    controller.Move(Vector3.up * yVelocity * Time.deltaTime);
    yVelocity += gravity * Time.deltaTime;
  }

  void Rotate()
  {
    playerBody.Rotate(inputRotation.x * Vector3.up * lookSensitivity * Time.deltaTime);

    xRotation -= (inputRotation.y * lookSensitivity * Time.deltaTime);
    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    Vector3 angles = camTransform.rotation.eulerAngles;
    angles.x = xRotation;
    camTransform.rotation = Quaternion.Euler(angles);
  }

  void OnEnable()
  {
    controls.Gameplay.Enable();
  }

  void OnDisable()
  {
    controls.Gameplay.Disable();
  }
}
