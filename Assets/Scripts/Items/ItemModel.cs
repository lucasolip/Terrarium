using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemModel : ScriptableObject
{
    public string itemName, itemTag;
    [TextArea]
    public string description;
    public int price;
    public Texture model;
    public bool isPlantable = false;
    public GameObject plantPrefab;
}
