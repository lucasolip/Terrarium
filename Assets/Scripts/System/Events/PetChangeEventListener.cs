using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PetChangeEventListener
{
    public abstract void OnPetChange(int hunger, int energy, int happiness, int cleanliness);
}
