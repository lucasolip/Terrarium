using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PetChangeEventListener : MonoBehaviour
{
    public PetChangeEvent petChangeEvent;
    public abstract void OnPetChange(int hunger, int energy, int happiness, int cleanliness);

    private void OnEnable()
    {
        petChangeEvent.petChangeEvent += OnPetChange;
    }

    private void OnDisable()
    {
        petChangeEvent.petChangeEvent -= OnPetChange;
    }
}
