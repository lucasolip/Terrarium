using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPoser : MonoBehaviour
{
    Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(cam.transform);
        transform.forward = cam.transform.forward;
    }
}
