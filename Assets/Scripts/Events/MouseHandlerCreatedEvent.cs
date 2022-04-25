using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/MouseHandlerCreated")]
public class MouseHandlerCreatedEvent : ScriptableObject
{
    public event Action mouseHandlerCreatedEvent;
    private List<MouseHandlerCreatedEventListener> listeners = new List<MouseHandlerCreatedEventListener>();

    public void Raise()
    {
        if (mouseHandlerCreatedEvent != null) mouseHandlerCreatedEvent();
    }
}
