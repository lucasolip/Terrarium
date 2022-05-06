using UnityEngine;

public class MouseFollower : MouseHandler
{
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

    public override void Clicked()
    {
        rb.isKinematic = true;
    }

    public override void Dragged()
    {
        transform.position = MathUtils.GetXZPlaneIntersection(Input.mousePosition, height, Camera.main);
    }

    public override void Released()
    {
        rb.isKinematic = false;
    }
}