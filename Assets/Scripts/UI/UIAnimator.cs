using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimator : MonoBehaviour
{
    public float duration = .5f;
    public float inDuration = .5f;
    public float outDuration = .5f;
    public LeanTweenType ease;
    public LeanTweenType inEase;
    public LeanTweenType outEase;
    public Vector3 inTarget;
    public Vector3 outTarget;

    public void ScaleIn(GameObject element, System.Action action)
    {
        LeanTween.scale(element, inTarget, inDuration).setOnComplete(action).setEase(inEase);
    }

    public void ScaleIn(GameObject element)
    {
        LeanTween.scale(element, inTarget, inDuration).setEase(inEase);
    }

    public void ScaleOut(GameObject element, System.Action action)
    {
        LeanTween.scale(element, outTarget, outDuration).setOnComplete(action).setEase(outEase);
    }

    public void ScaleOut(GameObject element)
    {
        LeanTween.scale(element, outTarget, outDuration).setEase(outEase);
    }

    public void Move(RectTransform element, Vector3 position)
    {
        LeanTween.move(element, position, duration).setEase(ease);
    }
}
