using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadMood : PetMood
{
    private const int fineThreshold = 30;

    public override void OnEnter(PetStage stage, PetMood previousMood, Material material)
    {
        material.mainTexture = stage.sadModel;
    }

    public override PetMood UpdateParameters(PetController pet, PetStage stage)
    {
        pet.hunger -= stage.hungerPerTick;
        pet.happiness -= stage.happinessPerTick;
        pet.energy -= stage.energyPerTick;
        if (pet.energy < 10)
        {
            return new AsleepMood();
        }
        if (pet.hunger > fineThreshold && pet.happiness > fineThreshold)
        {
            return new FineMood();
        }
        return null;
    }
}
