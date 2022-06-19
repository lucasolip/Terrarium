using UnityEngine;
using UnityEngine.EventSystems;

enum Mode {
    Click, Drag, Release
}

public class MouseController : MouseHandlerCreatedEventListener
{
    public LayerMask layerMask;
    public static ToolController selectedTool;
    Camera cam;
    public MouseHandler cameraControl;
    public MouseHandler selected;
    bool clicked = false;


    private void Start()
    {
        cam = Camera.main;
        if (cameraControl == null) Debug.LogWarning("MouseController no tiene acceso a CameraController");
        if (Input.GetMouseButton(0)) {
            CheckHit(Mode.Click);
        }
    }
    private void Update()
    {
        if (!clicked && Input.GetMouseButtonDown(0))
        {
            CheckHit(Mode.Click);
            clicked = true;
        }
        if (clicked && selected != null)
        {
            selected.Dragged();
        }
        if (clicked && Input.GetMouseButtonUp(0))
        {
            CheckHit(Mode.Release);
            clicked = false;
            selected = null;
        }
    }
    private void CheckHit(Mode mode)
    {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000f, layerMask))
        {
            MouseHandler handler = hit.transform.GetComponent<MouseHandler>();
            if (null != handler && handler.enabled) {
                switch (mode) {
                    case Mode.Click:
                        if (selected == null) {
                            selected = handler;
                            handler.Clicked();
                        }
                        break;
                    case Mode.Release:
                        handler.Released();
                        break;
                }
            }
        } else { // If nothing is pressed, CameraController takes action
            switch (mode) {
                case Mode.Click:
                    if (selected == null) {
                        selected = cameraControl;
                        cameraControl.Clicked();
                    }
                    break;
                case Mode.Release:
                    cameraControl.Released();
                    break;
            }
        }
    }

    public override void OnHandlerCreated()
    {
        selected = null;
        CheckHit(Mode.Click);
    }

    public void SetTool(ToolController tool)
    {
        selectedTool = tool;
        layerMask = tool.layerMask;
    }
}
