using System;
using System.Collections.Generic;

[System.Serializable]
public class InventoryData
{
    int[] itemIndices;
    int[] itemQuantities;
    int money;

    public InventoryData(InventoryController inventory) {
        Dictionary<ItemModel, int> items = inventory.GetItems();
        itemIndices = new int[items.Count];
        itemQuantities = new int[items.Count];
        int i = 0;
        foreach (KeyValuePair<ItemModel, int> pair in items) {
            itemIndices[i] = ScriptableObjectLocator.GetIndex(pair.Key);
            itemQuantities[i] = pair.Value;
            i++;
        }
        money = inventory.GetMoney();
    }

    public void LoadInventory(InventoryController inventory) {
        inventory.SetMoney(money);
        for (int i = 0; i < itemIndices.Length; i++) {
            ItemModel item = (ItemModel)ScriptableObjectLocator.Get(itemIndices[i]);
            inventory.SetItem(item, itemQuantities[i]);
        }
    }
}
