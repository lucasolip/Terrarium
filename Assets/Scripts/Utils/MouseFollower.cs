using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    public LayerMask layerMask;
    bool mouseDown = false;
    public float height = 1f;

    private void Awake()
    {
        layerMask = 1 << gameObject.layer;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), layerMask))
        {
            mouseDown = true;
        }
    }

    private void Update()
    {
        if (mouseDown)
        {
            if (!Input.GetMouseButton(0))
            {
                mouseDown = false;
            }
            transform.position = MathUtils.GetXZPlaneIntersection(Input.mousePosition, height, Camera.main);
        }
    }

    private void OnMouseDown()
    {
        mouseDown = true;
    }

    private void OnMouseUp()
    {
        mouseDown = false;
    }
}