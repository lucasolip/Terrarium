using System;
using UnityEditor;

[System.Serializable]
public class PetData
{
    int hunger, energy, happiness, cleanliness;
    string petName;
    DateTime bornTime;
    string stagePath;

    public PetData(PetController pet) {
        hunger = pet.hunger;
        energy = pet.energy;
        happiness = pet.happiness;
        cleanliness = pet.cleanliness;
        petName = pet.petName;
        bornTime = pet.bornTime;
        stagePath = AssetDatabase.GetAssetPath(pet.stage);
    }

    public void LoadPet(PetController pet) {
        pet.hunger = hunger;
        pet.energy = energy;
        pet.happiness = happiness;
        pet.cleanliness = cleanliness;
        pet.petName = petName;
        pet.bornTime = bornTime;
        pet.stage = (PetStage)AssetDatabase.LoadAssetAtPath(stagePath, typeof(PetStage));
    }
}