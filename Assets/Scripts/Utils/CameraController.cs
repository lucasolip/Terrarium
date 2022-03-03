using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public float nearClipPlane = -15;
    public float ySensitivity = .001f;
    public float xSensitivity = 1f;
    [Range(0f,1f)]
    public float minY = 0.05f;
    [Range(0f,1f)]
    public float maxY = 1f;
    private Vector3 origin;
    private Vector3 difference;
    private Vector3 resetCamera;
    private bool drag = false;
    private bool frozen;
    CinemachineFreeLook cam;
    void Start()
    {
        cam = GetComponent<CinemachineFreeLook>();
        cam.m_Lens.NearClipPlane = nearClipPlane;
        cam.m_YAxis.Value = 0.5f;
    }

    void LateUpdate()
    {
        if (!frozen) {
            if (Input.GetMouseButton(0)) {
                if (!drag) {
                    origin = Input.mousePosition;
                    drag = true;
                }
            } else {
                drag = false;
            }
            if (drag) {
                difference = Input.mousePosition - origin;
                cam.m_XAxis.Value = difference.x * xSensitivity;
                cam.m_YAxis.Value -= difference.y * ySensitivity;
                cam.m_YAxis.Value = Mathf.Clamp(cam.m_YAxis.Value, minY, maxY);
                origin = Input.mousePosition;
            }
        }
    }

    public void Freeze() {
        frozen = true;
        Debug.Log("Camera frozen");
    }

    public void Unfreeze() {
        frozen = false;
        origin = Input.mousePosition;
        Debug.Log("Camera free");
    }
}
