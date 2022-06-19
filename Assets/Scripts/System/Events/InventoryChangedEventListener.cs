using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InventoryChangedEventListener
{
    public abstract void OnItemAdded(ItemModel item);
    public abstract void OnItemChanged(ItemModel item, int quantity);
    public abstract void OnItemRemoved(ItemModel item);
}
