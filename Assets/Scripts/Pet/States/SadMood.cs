using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadMood : PetMood {
    private const int fineThreshold = 30;

    public override void OnEnter(PetStage stage, PetMood previousMood, Material material) {
        material.mainTexture = stage.sadModel;
    }

    public override void ComputeNecessities(PetController pet, PetStage stage) {
        pet.hunger -= stage.hungerPerTick;
        pet.happiness -= stage.happinessPerTick;
        pet.energy -= stage.energyPerTick;
    }

    public override PetMood CheckMood(PetController pet, PetStage stage) {
        if (pet.energy < 10) {
            return new AsleepMood();
        }
        if (pet.hunger > fineThreshold && pet.happiness > fineThreshold) {
            return new FineMood();
        }
        return null;
    }
}
