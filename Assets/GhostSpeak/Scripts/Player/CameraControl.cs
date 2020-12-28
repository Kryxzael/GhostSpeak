using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

[AddComponentMenu("Camera-Control/Camera Controller")]
public class CameraControl : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    public float moveSpeed = 2.0f;

    public float minTurnAngleHori = -90.0f;
    public float maxTurnAngleHori = 90.0f;

    public float minTurnAngleVerti = -90.0f;
    public float maxTurnAngleVerti = 90.0f;

    private float rotX;
    private float rotY;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MouseAiming();
        KeyboardMovement();
    }

    void MouseAiming()
    {
        // get the mouse inputs
        rotY += Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;

        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngleHori, maxTurnAngleHori);
        rotY = Mathf.Clamp(rotY, minTurnAngleVerti, maxTurnAngleVerti);

        // rotate the camera
        transform.eulerAngles = new Vector3(-rotX, rotY, 0);
    }

    void KeyboardMovement()
    {
        Vector3 dir = new Vector3(
            x: Input.GetAxis("Horizontal"),
            y: 0,
            z: Input.GetAxis("Vertical")
        );

        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }
}