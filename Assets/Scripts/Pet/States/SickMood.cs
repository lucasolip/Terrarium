using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickMood : PetMood
{
    public PetMood previousMood;
    const int timeBeingSick = 120;
    int timeSick = 0;
    public override void OnEnter(PetStage stage, PetMood previousMood, Material material)
    {
        this.previousMood = previousMood;
        material.mainTexture = stage.sickModel;
    }

    public override PetMood UpdateParameters(PetController pet, PetStage stage)
    {
        pet.hunger -= stage.hungerPerTick;
        pet.happiness -= stage.happinessPerTick;
        pet.energy -= stage.energyPerTick;
        timeSick++;
        if (pet.energy < 10)
        {
            return new AsleepMood();
        }
        if (timeSick > timeBeingSick) pet.Die();
        return null;
    }
}
