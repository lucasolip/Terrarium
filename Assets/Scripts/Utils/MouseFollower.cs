using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    public LayerMask layerMask;
    bool drag = false;
    public float height = 1f;
    CameraController cam;

    private void Awake()
    {
        layerMask = 1 << gameObject.layer;
    }

    private void Start() {
        cam = GameObject.Find("VirtualCamera").GetComponent<CameraController>();
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), layerMask))
        {
            drag = true;
            cam.Freeze();
        }
    }

    private void Update()
    {
        if (drag)
        {
            if (!Input.GetMouseButton(0))
            {
                drag = false;
            }
            transform.position = MathUtils.GetXZPlaneIntersection(Input.mousePosition, height, Camera.main);
        }
    }

    private void OnMouseDown()
    {
        drag = true;
        cam.Freeze();
    }

    private void OnMouseUp()
    {
        drag = false;
        cam.Unfreeze();
    }

    private void OnDestroy() {
        cam.Unfreeze();
    }
}