using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListUI : MonoBehaviour
{
    [Header("Dependencies")]
    public InventoryController userInventory;
    public GameObject itemElementPrefab;
    public ItemInfoUI info;
    [Header("Element Style")]
    public Color backgroundElementColor, selectedElementColor;
    protected Dictionary<ItemModel, ItemElementUI> itemElements;
    protected ItemElementUI selected;

    protected void Awake()
    {
        itemElements = new Dictionary<ItemModel, ItemElementUI>();
    }

    public void Clicked(ItemElementUI element)
    {
        
    }

    public void Released(ItemElementUI element)
    {
        if (selected != null) 
            selected.SetBackgroundColor(backgroundElementColor);
        selected = element;
        element.SetBackgroundColor(selectedElementColor);
        info.SetInfo(element.item.itemName, element.item.description, element.item.model);
    }

    protected void AddItemElement(ItemModel item, int quantity)
    {
        ItemElementUI itemElement = Instantiate(itemElementPrefab, transform).GetComponent<ItemElementUI>();
        itemElement.item = item;
        itemElement.SetIcon(item.model);
        itemElement.SetBackgroundColor(backgroundElementColor);
        itemElement.SetQuantity(quantity);
        itemElement.itemList = this;
        itemElements.Add(item, itemElement);
    }

    
}
