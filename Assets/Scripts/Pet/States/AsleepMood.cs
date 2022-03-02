using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsleepMood : PetMood
{
    public PetMood previousMood;

    public override void OnEnter(PetStage stage, PetMood previousMood, Material material)
    {
        this.previousMood = previousMood;
        material.mainTexture = stage.asleepModel;
    }

    public override PetMood UpdateParameters(PetController pet, PetStage stage)
    {
        pet.energy += stage.energyRecoveryAsleep;
        if (pet.energy > 99)
        {
            return previousMood;
        }
        return null;
    }
}
