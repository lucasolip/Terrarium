using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public int initialMoney = 100;
    private int money;
    public MoneyChangedEvent moneyChangedEvent;
    public InventoryChangedEvent inventoryChangedEvent;
    public ItemModel[] initialItems;
    private Dictionary<ItemModel, int> items;
    private UIAudioPlayer player;

    private void Awake()
    {
        player = GetComponent<UIAudioPlayer>();
        items = new Dictionary<ItemModel, int>();
        InventoryData data = SaveSystem.LoadInventory();
        if (data != null) {
            data.LoadInventory(this);
        } else {
            InitializeInventory();
        }
    }

    public void InitializeInventory() {
        SetMoney(initialMoney);
        foreach (ItemModel item in initialItems) {
            AddItem(item);
        }
    }

    public void AddItem(ItemModel item)
    {
        if (!items.ContainsKey(item)) {
            items.Add(item, 1);
            SaveSystem.SaveInventory(this);
            inventoryChangedEvent.RaiseAdded(item);
            return;
        }
        items[item]++;
        SaveSystem.SaveInventory(this);
        inventoryChangedEvent.RaiseChanged(item, items[item]);
    }

    public void RemoveItem(ItemModel item)
    {
        items[item]--;
        if (items[item] == 0) {
            items.Remove(item);
            SaveSystem.SaveInventory(this);
            inventoryChangedEvent.RaiseRemoved(item);
            return;
        }
        inventoryChangedEvent.RaiseChanged(item, items[item]);
    }

    public void ClearItems() {
        ItemModel[] keys = items.Keys.ToArray();
        foreach (ItemModel item in keys) {
            items.Remove(item);
            inventoryChangedEvent.RaiseRemoved(item);
        }
    }

    public void SetItem(ItemModel item, int quantity) {
        items.Add(item, quantity);
        inventoryChangedEvent.RaiseAdded(item);
        if (quantity > 1) inventoryChangedEvent.RaiseChanged(item, quantity);
    }

    public void AddMoney(int quantity)
    {
        money += quantity;
        SaveSystem.SaveInventory(this);
        moneyChangedEvent.Raise(money);
        player.Play(0);
    }

    public bool SubstractMoney(int quantity)
    {
        if (money - quantity < 0) {
            player.Play(1);
            return false;
        }
        money -= quantity;
        SaveSystem.SaveInventory(this);
        moneyChangedEvent.Raise(money);
        player.Play(0);
        return true;
    }

    public void SetMoney(int quantity) {
        this.money = quantity;
        moneyChangedEvent.Raise(money);
    }

    public int GetMoney() {
        return money;
    }

    public Dictionary<ItemModel, int> GetItems()
    {
        return items;
    }

    public void ResetInventory() {
        SaveSystem.ResetInventory();
        ClearItems();
        InitializeInventory();
    }
}
