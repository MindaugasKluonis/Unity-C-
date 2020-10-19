using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5f;
    public float minBorderX, maxBorderX, minBorderY, maxBorderY;

    private Camera _currentCamera;
    private Vector3 cameraVelocity = Vector3.zero;

    private void Awake()
    {
        _currentCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        Vector3 pos = _currentCamera.ScreenToViewportPoint(Input.mousePosition);

        UpdateVerticalCameraPosition(pos);
        UpdateHorizontalCameraPosition(pos);

        if (cameraVelocity != Vector3.zero)
            _currentCamera.transform.Translate(cameraVelocity * Time.smoothDeltaTime);
    }

    private void UpdateHorizontalCameraPosition(Vector3 pos)
    {
        float x = pos.x;

        if(x < minBorderX)
        {
            cameraVelocity.x = -cameraSpeed;
            return;
        }

        else if(x > maxBorderX)
        {
            cameraVelocity.x = cameraSpeed;
            return;
        }

        cameraVelocity.x = 0f;
    }

    private void UpdateVerticalCameraPosition(Vector3 pos)
    {
        float y = pos.y;

        if (y < minBorderY)
        {
            cameraVelocity.y = -cameraSpeed;
            return;
        }

        else if (y > maxBorderY)
        {
            cameraVelocity.y = cameraSpeed;
            return;
        }

        cameraVelocity.y = 0f;
    }
}
