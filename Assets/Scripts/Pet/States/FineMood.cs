using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FineMood : PetMood
{
    private const int sadThreshold = 30;

    public override void OnEnter(PetStage stage, PetMood previousMood, Material material)
    {
        material.mainTexture = stage.fineModel;
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
        if (pet.hunger < sadThreshold || pet.happiness < sadThreshold)
        {
            return new SadMood();
        }
        return null;
    }
}