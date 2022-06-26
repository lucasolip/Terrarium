using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetController : MonoBehaviour
{
    public Transform target;
    private void LateUpdate()
    {
        if (target != null) transform.position = target.position;
    }
}
