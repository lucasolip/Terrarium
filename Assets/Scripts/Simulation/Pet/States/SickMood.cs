using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickMood : PetMood {
    public PetMood previousMood;
    int timeSick = 0;
    public override void OnEnter(PetStage stage, PetMood previousMood, Material material) {
        this.previousMood = previousMood;
        material.mainTexture = stage.sickModel;
    }

    public override PetMood CheckMood(PetController pet, PetStage stage) {
        Debug.Log("Sick check");
        if (pet.energy < 10) {
            return new AsleepMood();
        }
        if (timeSick > stage.sickTime) pet.Die();
        return null;
    }

    public override void ComputeNecessities(PetController pet, PetStage stage) {
        pet.hunger -= stage.hungerPerTick;
        pet.happiness -= stage.happinessPerTick;
        pet.energy -= stage.energyPerTick;
        timeSick++;
    }

    public override void Eat(PetController pet, ItemController item) {
        if (item.model is FoodModel) {
            if (Random.Range(0f, 1f) < .5f) {
                Debug.Log(pet.petName + " rechazó la comida");
                return;
            }
            FoodModel food = (FoodModel)item.model;
            pet.hunger += food.hunger;
            pet.happiness += food.happiness;
            pet.energy += food.energy;
            pet.ClampParameters();
            pet.poops += pet.stage.poopsPerMeal;
            GameObject.Destroy(item.gameObject);
        } else if (item.model.itemTag.Equals("Medicine")) {
            pet.mood = new FineMood();
            GameObject.Destroy(item.gameObject);
        }
        pet.CheckMood();
    }

    public override Texture GetModel(PetStage stage)
    {
        return stage.sickModel;
    }
}
