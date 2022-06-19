using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BuyUI : ItemListUI
{
    [Header("Shop Items")]
    public int numberItemsForSale;
    public ItemModel[] availableItems;
    public ItemModel[] essentialItems;

    private List<ItemModel> itemsForSale;

    new void Awake()
    {
        base.Awake();
        itemElements = new Dictionary<ItemModel, ItemElementUI>();
        itemsForSale = new List<ItemModel>();
        FillItemsForSale();
        foreach (ItemModel item in itemsForSale) {
            AddItemElement(item, item.price);
        }
    }

    private void FillItemsForSale()
    {
        itemsForSale.Clear();
        List<ItemModel> availableItemsCopy = availableItems.ToList();
        for (int i = 0; i < numberItemsForSale; i++)
        {
            itemsForSale.Add(availableItemsCopy[Mathf.FloorToInt(Random.Range(0, availableItemsCopy.Count))]);
            availableItemsCopy.Remove(itemsForSale[i]);
        }
        foreach (ItemModel item in essentialItems)
        {
            itemsForSale.Add(item);
        }
    }

    public void Buy()
    {
        if (selected == null) {
            Debug.LogError("Nothing is selected");
            return;
        }
        bool validOperation = userInventory.SubstractMoney(selected.item.price);
        if (validOperation) userInventory.AddItem(selected.item);
    }
}
