using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FineMood : PetMood {
    private const int sadThreshold = 30;

    public override void OnEnter(PetStage stage, PetMood previousMood, Material material) {
        material.mainTexture = stage.fineModel;
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
        if (pet.hunger < sadThreshold || pet.happiness < sadThreshold) {
            return new SadMood();
        }
        return null;
    }

    public override Texture GetModel(PetStage stage)
    {
        return stage.fineModel;
    }
}