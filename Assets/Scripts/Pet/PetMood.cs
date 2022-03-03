using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PetMood
{
    public abstract void OnEnter(PetStage stage, PetMood previousMood, Material material);
    public abstract PetMood UpdateParameters(PetController pet, PetStage stage);

    public virtual void Eat(PetController pet, Food food)
    {
        pet.hunger += food.foodModel.hunger;
        pet.happiness += food.foodModel.happiness;
        pet.energy += food.foodModel.energy;
        pet.ClampParameters();
        pet.poops += pet.stage.poopsPerMeal;
        GameObject.Destroy(food.gameObject);
    }
}
