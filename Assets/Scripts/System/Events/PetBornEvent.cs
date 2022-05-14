using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Event/PetBorn")]
public class PetBornEvent : ScriptableObject
{
    public event Action<PetController> petBornEvent;
    private List<PetBornEventListener> listeners = new List<PetBornEventListener>();

    public void Raise(PetController pet)
    {
        if (petBornEvent != null) petBornEvent(pet);
    }
}
