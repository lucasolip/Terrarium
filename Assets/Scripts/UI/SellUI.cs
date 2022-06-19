using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellUI : ItemListUI, InventoryChangedEventListener
{
    public InventoryChangedEvent inventoryChangedEvent;
    public float priceMultiplier = 0.5f;

    private void Start()
    {
        if (itemElements.Count == 0) {
            Dictionary<ItemModel, int> items = userInventory.GetItems();
            foreach (ItemModel item in items.Keys) {
                AddItemElement(item, GetItemPrice(item));
            }
        }
    }

    private int GetItemPrice(ItemModel item)
    {
        return Mathf.FloorToInt(item.price * priceMultiplier);
    }

    public void OnItemAdded(ItemModel item)
    {
        AddItemElement(item, GetItemPrice(item));
    }

    public void OnItemChanged(ItemModel item, int quantity) {}

    public void OnItemRemoved(ItemModel item)
    {
        ItemElementUI itemElement = itemElements[item];
        itemElements.Remove(item);
        Destroy(itemElement.gameObject);
    }

    public void Sell()
    {
        if (selected == null) {
            Debug.LogError("Nothing is selected");
            return;
        }
        userInventory.AddMoney(GetItemPrice(selected.item));
        userInventory.RemoveItem(selected.item);
    }

    private void OnEnable()
    {
        inventoryChangedEvent.itemAdded += OnItemAdded;
        inventoryChangedEvent.itemRemoved += OnItemRemoved;
    }

    private void OnDisable()
    {
        inventoryChangedEvent.itemAdded -= OnItemAdded;
        inventoryChangedEvent.itemRemoved -= OnItemRemoved;
    }
}
