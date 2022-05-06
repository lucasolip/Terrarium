using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PetBornEventListener
{
    public abstract void OnPetBorn(PetController pet);
}
