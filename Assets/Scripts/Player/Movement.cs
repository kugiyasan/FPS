using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
  [Header("Character Movement")]
  public CharacterController controller;
  public Transform playerBody; // Should be equal to transform
  public float walkSpeed = 20f;
  public float sprintSpeed = 40f;

  Vector2 inputMovement;
  float yVelocity = 0f;
  bool isSprinting;

  public void Move(InputAction.CallbackContext ctx)
  {
    inputMovement = ctx.ReadValue<Vector2>();
    Debug.Log("Move is called!!");
  }

  void Update()
  {
    Debug.Log("Move Update is called!!");
    // ! This statement should be called once, not every frame
    if (inputMovement == Vector2.zero)
      isSprinting = false;

    Vector3 m = inputMovement.x * playerBody.right + inputMovement.y * playerBody.forward;
    float speed = isSprinting ? sprintSpeed : walkSpeed;

    controller.Move(m * speed * Time.deltaTime);
  }
}