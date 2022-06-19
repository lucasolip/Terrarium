using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryListUI : ItemListUI, InventoryChangedEventListener
{
    public InventoryChangedEvent inventoryChangedEvent;

    private void Start()
    {
        if (itemElements.Count == 0) {
            Dictionary<ItemModel, int> items = userInventory.GetItems();
            foreach (KeyValuePair<ItemModel, int> pair in items)
            {
                AddItemElement(pair.Key, pair.Value);
            }
        }
    }

    public void OnItemAdded(ItemModel item)
    {
        AddItemElement(item, 1);
    }

    public void OnItemChanged(ItemModel item, int quantity)
    {
        itemElements[item].SetQuantity(quantity);
    }

    public void OnItemRemoved(ItemModel item)
    {
        ItemElementUI itemElement = itemElements[item];
        itemElements.Remove(item);
        Destroy(itemElement.gameObject);
    }

    private void OnEnable()
    {
        inventoryChangedEvent.itemAdded += OnItemAdded;
        inventoryChangedEvent.itemChanged += OnItemChanged;
        inventoryChangedEvent.itemRemoved += OnItemRemoved;
    }

    private void OnDisable()
    {
        inventoryChangedEvent.itemAdded -= OnItemAdded;
        inventoryChangedEvent.itemChanged -= OnItemChanged;
        inventoryChangedEvent.itemRemoved -= OnItemRemoved;
    }
}
