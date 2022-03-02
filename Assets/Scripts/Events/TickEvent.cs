using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/TickEvent")]
public class TickEvent : ScriptableObject
{
    public event Action tickEvent;
    private List<TickEventListener> listeners = new List<TickEventListener>();

    public void Raise()
    {
        tickEvent();
    }
}
