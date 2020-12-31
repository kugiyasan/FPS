using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    [Header("Character Look Around")]
    public Transform playerBody; // Should be equal to transform
    public Transform camTransform;
    public float lookSensitivity = 20f;

    Vector2 inputRotation;
    float xRotation = 0f;

    public void Rotate(InputAction.CallbackContext ctx)
    {
        inputRotation = ctx.ReadValue<Vector2>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        playerBody.Rotate(inputRotation.x * Vector3.up * lookSensitivity * Time.deltaTime);

        xRotation -= (inputRotation.y * lookSensitivity * Time.deltaTime);
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Vector3 angles = camTransform.rotation.eulerAngles;
        angles.x = xRotation;
        camTransform.rotation = Quaternion.Euler(angles);
    }
}