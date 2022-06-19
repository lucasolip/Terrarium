using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Event/InventoryChanged")]
public class InventoryChangedEvent : ScriptableObject
{
    public event Action<ItemModel> itemAdded;
    public event Action<ItemModel, int> itemChanged;
    public event Action<ItemModel> itemRemoved;

    public void RaiseAdded(ItemModel item)
    {
        if (itemAdded != null) itemAdded(item);
    }

    public void RaiseChanged(ItemModel item, int quantity)
    {
        if (itemChanged != null) itemChanged(item, quantity);
    }

    public void RaiseRemoved(ItemModel item)
    {
        if (itemRemoved != null) itemRemoved(item);
    }
}
