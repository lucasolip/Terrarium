using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private Vector3 origin;
    private Vector3 difference;
    private Vector3 resetCamera;
    private bool drag = false;
    Camera cam;
    CinemachineFreeLook cfl;
    void Start()
    {
        cam = Camera.main;
        resetCamera = cam.transform.position;
        cfl = GetComponent<CinemachineFreeLook>();
    }

    void LateUpdate()
    {
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
            cfl.m_XAxis.Value = difference.x;
            //cfl.m_YAxis.Value = MathUtils.Map(Input.mousePosition.y, 0, Screen.height, 0, 2);
            origin = Input.mousePosition;
        }
        
    }
}
