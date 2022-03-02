using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Food")]
public class FoodModel : ScriptableObject
{
    public string foodName, description;
    public Texture model;
    public int hunger, energy, happiness;
}
