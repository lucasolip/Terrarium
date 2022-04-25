using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : MouseHandlerCreatedEventListener
{
    public LayerMask layerMask;
    Camera cam;
    bool clicked = false;

    private void Start()
    {
        cam = Camera.main;
        if (Input.GetMouseButton(0)) {
            CheckHit(true);
        }
    }
    private void Update()
    {
        if (!clicked && Input.GetMouseButtonDown(0)) {
            CheckHit(true);
            clicked = true;
        }
        if (clicked && Input.GetMouseButtonUp(0)) {
            CheckHit(false);
            clicked = false;
        }
    }
    private void CheckHit(bool down)
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000f, layerMask)) {
            MouseHandler handler = hit.transform.GetComponent<MouseHandler>();
            if (null != handler && handler.enabled) {
                if (down) {
                    handler.Clicked();
                } else if (!down) {
                    handler.Released();
                }
            }
        }
    }

    public override void OnHandlerCreated()
    {
        CheckHit(true);
    }
}
