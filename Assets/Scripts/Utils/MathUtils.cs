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

    public static float Map(float value, float istart, float istop, float ostart, float ostop) {
        return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
    }
}
