using UnityEngine;

public class MouseFollower : MouseHandler
{
    bool drag = false;
    public float height = 1f;
    CameraController cam;
    Rigidbody rb;
    public MouseHandlerCreatedEvent createdEvent;

    private void Awake() {
        cam = GameObject.Find("VirtualCamera").GetComponent<CameraController>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        createdEvent.Raise();
    }

    private void Update()
    {
        if (drag) {
            transform.position = MathUtils.GetXZPlaneIntersection(Input.mousePosition, height, Camera.main);
        }
    }

    private void OnDestroy() {
        cam.Unfreeze();
    }

    public override void Clicked()
    {
        drag = true;
        cam.Freeze();
        rb.isKinematic = true;
    }

    public override void Released()
    {
        drag = false;
        cam.Unfreeze();
        rb.isKinematic = false;
    }
}