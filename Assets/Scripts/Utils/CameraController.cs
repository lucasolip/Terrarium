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
    public float mobileZoomSpeed = 0.001f;
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
#if UNITY_ANDROID
        if (Input.touchCount >= 2) {
            Touch tZero = Input.GetTouch(0);
            Touch tOne = Input.GetTouch(1);
            Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
            Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

            float oldTouchDistance = Vector2.Distance (tZeroPrevious, tOnePrevious);
            float currentTouchDistance = Vector2.Distance (tZero.position, tOne.position);

            float deltaDistance = oldTouchDistance - currentTouchDistance;
            cam.m_Lens.OrthographicSize = Mathf.Clamp(cam.m_Lens.OrthographicSize + deltaDistance * mobileZoomSpeed, minZoom, maxZoom);
        }
#else
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) {
            cam.m_Lens.OrthographicSize = Mathf.Clamp(cam.m_Lens.OrthographicSize - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, minZoom, maxZoom);
        }
#endif
    }

    public override void Clicked()
    {
        if (Input.touchCount < 2) origin = Input.mousePosition;
    }

    public override void Dragged()
    {
        if (Input.touchCount < 2) {
            difference = Input.mousePosition - origin;
            cam.m_XAxis.Value += difference.x * xSensitivity;
            cam.m_YAxis.Value -= difference.y * ySensitivity;
            cam.m_YAxis.Value = Mathf.Clamp(cam.m_YAxis.Value, minY, maxY);
            origin = Input.mousePosition;
        }
    }

    public override void Released()
    {
        if (Input.touchCount < 2) origin = Input.mousePosition;
    }
}
