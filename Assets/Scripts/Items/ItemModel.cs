using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemModel : ScriptableObject
{
    public string itemName, description, itemTag;
    public Texture model;
}
