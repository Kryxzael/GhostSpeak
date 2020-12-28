using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

[RequireComponent(typeof(Camera))]
[AddComponentMenu("Camera-Control/Camera Controller")]
public class CameraControl : MonoBehaviour
{
    private Camera _cam;

    public float turnSpeed = 4.0f;

    public float minTurnAngleHori = -90.0f;
    public float maxTurnAngleHori = 90.0f;

    public float minTurnAngleVerti = -90.0f;
    public float maxTurnAngleVerti = 90.0f;

    private float rotX;
    private float rotY;

    public float ZoomedFOV = 30f;
    public float NormalFOV = 60f;

    public bool IsZoomed;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _cam = GetComponent<Camera>();
    }

    void Update()
    {
        MouseAiming();
        Zooming();
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

    void Zooming()
    {
        if (Input.GetButtonDown("Zoom"))
        {
            StopAllCoroutines();

            if (IsZoomed)
            {
                StartCoroutine(zoomOut());
            }
            else
            {
                
                StartCoroutine(zoomIn());
            }
        }

        IEnumerator zoomIn()
        {
            IsZoomed = true;
            while (_cam.fieldOfView > ZoomedFOV)
            {
                _cam.fieldOfView--;
                yield return new WaitForEndOfFrame();
            }
        }

        IEnumerator zoomOut()
        {
            IsZoomed = false;
            while (_cam.fieldOfView < NormalFOV)
            {
                _cam.fieldOfView++;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}