using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class Tab : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public Image background;
    public TabGroup group;


    void Start()
    {
        background = GetComponent<Image>();
        group.Subscribe(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        group.OnClick(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        group.OnRelease(this);
    }

}
