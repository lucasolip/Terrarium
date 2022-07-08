using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VerticalLayoutGroup))]
public class VerticalAutoHeight : MonoBehaviour
{
    public float elementHeight;
    VerticalLayoutGroup group;
    RectTransform rectTransform;

    private void Awake()
    {
        group = GetComponent<VerticalLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
        Resize();
    }

    public void Resize()
    {
        int numElements = transform.childCount;
        float groupHeight = elementHeight * numElements + group.spacing * numElements + group.padding.top + group.padding.bottom;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, groupHeight);
    }
}
