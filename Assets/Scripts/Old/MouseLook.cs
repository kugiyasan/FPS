﻿using UnityEngine;

public class MouseLook : MonoBehaviour
{
  public Transform playerBody;
  public float mouseSensitivity = 300f;

  float xRotation = 0f;

  void Start()
  {
    Cursor.lockState = CursorLockMode.Locked;
  }

  void Update()
  {
    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    // Horizontal rotation
    playerBody.Rotate(Vector3.up * mouseX);

    // Vertical rotation
    xRotation -= mouseY;
    xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
  }
}