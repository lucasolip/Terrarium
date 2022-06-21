using System;

[System.Serializable]
public class PetData
{
    int hunger, energy, happiness, cleanliness;
    string petName;
    DateTime bornTime;
    int stageIndex;
    bool dead;

    public PetData(PetController pet, bool dead) {
        this.dead = dead;
        hunger = pet.hunger;
        energy = pet.energy;
        happiness = pet.happiness;
        cleanliness = pet.cleanliness;
        petName = pet.petName;
        bornTime = pet.bornTime;
        stageIndex = ScriptableObjectLocator.GetIndex(pet.stage);
    }

    public void LoadPet(PetController pet) {
        pet.hunger = hunger;
        pet.energy = energy;
        pet.happiness = happiness;
        pet.cleanliness = cleanliness;
        pet.petName = petName;
        pet.bornTime = bornTime;
        pet.stage = (PetStage)ScriptableObjectLocator.Get(stageIndex);
    }

    public bool IsDead()
    {
        return dead;
    }
}