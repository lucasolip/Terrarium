using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Event/PetDied")]
public class PetDiedEvent : ScriptableObject
{
    public event Action<PetController> petDiedEvent;

    public void Raise(PetController pet)
    {
        if (petDiedEvent != null) petDiedEvent(pet);
    }
}
