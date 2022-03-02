using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtils
{
    public static Vector3 GetXZPlaneIntersection(Vector3 reference, float height, Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(reference);
        float delta = ray.origin.y - height;
        Vector3 dirNorm = ray.direction / ray.direction.y;
        return ray.origin - dirNorm * delta;
    }
}
