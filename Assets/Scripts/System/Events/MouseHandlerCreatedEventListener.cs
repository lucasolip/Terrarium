using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MouseHandlerCreatedEventListener : MonoBehaviour
{
    public MouseHandlerCreatedEvent handlerCreatedEvent;
    public abstract void OnHandlerCreated();

    private void OnEnable()
    {
        handlerCreatedEvent.mouseHandlerCreatedEvent += OnHandlerCreated;
    }

    private void OnDisable()
    {
        handlerCreatedEvent.mouseHandlerCreatedEvent -= OnHandlerCreated;
    }
}
