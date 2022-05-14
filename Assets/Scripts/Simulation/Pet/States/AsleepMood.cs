using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsleepMood : PetMood {
    public PetMood previousMood;

    public override void OnEnter(PetStage stage, PetMood previousMood, Material material) {
        this.previousMood = previousMood;
        material.mainTexture = stage.asleepModel;
    }

    public override void ComputeNecessities(PetController pet, PetStage stage) {
        pet.energy += stage.energyRecoveryAsleep;
    }

    public override PetMood CheckMood(PetController pet, PetStage stage) {
        if (pet.energy > 99) {
            return previousMood;
        }
        return null;
    }

    public override void Eat(PetController pet, ItemController food) {
        return;
    }
}
