using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MouseHandler
{
    public float nearClipPlane = -15;
    public float ySensitivity = .001f;
    public float xSensitivity = 1f;
    [Range(0f, 1f)]
    public float minY = 0.05f;
    [Range(0f, 1f)]
    public float maxY = 1f;
    public float minZoom = 1f;
    public float maxZoom = 10f;
    public float zoomSpeed = 1f;
    private Vector3 origin;
    private Vector3 difference;
    private Vector3 resetCamera;
    CinemachineFreeLook cam;
    void Start()
    {
        cam = GetComponent<CinemachineFreeLook>();
        cam.m_Lens.NearClipPlane = nearClipPlane;
        cam.m_YAxis.Value = 0.5f;
    }

    void LateUpdate()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) {
            cam.m_Lens.OrthographicSize = Mathf.Clamp(cam.m_Lens.OrthographicSize - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, minZoom, maxZoom);
        }
    }

    public override void Clicked()
    {
        origin = Input.mousePosition;
    }

    public override void Dragged()
    {
        difference = Input.mousePosition - origin;
        cam.m_XAxis.Value += difference.x * xSensitivity;
        cam.m_YAxis.Value -= difference.y * ySensitivity;
        cam.m_YAxis.Value = Mathf.Clamp(cam.m_YAxis.Value, minY, maxY);
        origin = Input.mousePosition;
    }

    public override void Released()
    {
        origin = Input.mousePosition;
    }
}
