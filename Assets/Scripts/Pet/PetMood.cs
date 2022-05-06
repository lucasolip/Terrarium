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

    public abstract void ComputeNecessities(PetController pet, PetStage stage);

    public abstract PetMood CheckMood(PetController pet, PetStage stage);

    public virtual void Eat(PetController pet, FoodController food)
    {
        pet.hunger += food.foodModel.hunger;
        pet.happiness += food.foodModel.happiness;
        pet.energy += food.foodModel.energy;
        pet.ClampParameters();
        pet.poops += pet.stage.poopsPerMeal;
        GameObject.Destroy(food.gameObject);
        pet.CheckMood();
    }
}
