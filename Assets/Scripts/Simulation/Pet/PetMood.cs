using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PetMood
{
    public abstract void OnEnter(PetStage stage, PetMood previousMood, Material material);
    public virtual PetMood UpdateParameters(PetController pet, PetStage stage) {
        ComputeNecessities(pet, stage);
        return CheckMood(pet, stage);
    }

    public abstract Texture GetModel(PetStage stage);

    public abstract void ComputeNecessities(PetController pet, PetStage stage);

    public abstract PetMood CheckMood(PetController pet, PetStage stage);

    public virtual void Eat(PetController pet, ItemController item)
    {
        if (item.model is FoodModel) {
            FoodModel food = (FoodModel)item.model;
            pet.hunger += food.hunger;
            pet.happiness += food.happiness;
            pet.energy += food.energy;
            pet.ClampParameters();
            pet.poops += pet.stage.poopsPerMeal;
            GameObject.Destroy(item.gameObject);
        }
        pet.CheckMood();
    }
}
