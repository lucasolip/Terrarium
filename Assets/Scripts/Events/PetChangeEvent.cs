using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Event/PetChange")]
public class PetChangeEvent : ScriptableObject
{
    public event Action<int, int, int, int> petChangeEvent;
    private List<PetChangeEventListener> listeners = new List<PetChangeEventListener>();

    public void Raise(int hunger, int energy, int happiness, int cleanliness)
    {
        if (petChangeEvent != null) petChangeEvent(hunger, energy, happiness, cleanliness);
    }
}
