using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadMood : PetMood {
    private const int fineThreshold = 30;
    private int hungerTime = 0;

    public override void OnEnter(PetStage stage, PetMood previousMood, Material material) {
        material.mainTexture = stage.sadModel;
    }

    public override void ComputeNecessities(PetController pet, PetStage stage) {
        pet.hunger -= stage.hungerPerTick;
        pet.happiness -= stage.happinessPerTick;
        pet.energy -= stage.energyPerTick;
        if (pet.hunger < 1) hungerTime++;
    }

    public override PetMood CheckMood(PetController pet, PetStage stage) {
        if (pet.energy < 10) {
            return new AsleepMood();
        }
        if (pet.hunger > fineThreshold && pet.happiness > fineThreshold) {
            return new FineMood();
        }
        if (hungerTime > stage.sickTime || pet.poops > stage.maxPoops) {
            return new SickMood();
        }
        return null;
    }
}
