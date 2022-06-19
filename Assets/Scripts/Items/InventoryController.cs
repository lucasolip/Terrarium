using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private int money = 100;
    public MoneyChangedEvent moneyChangedEvent;
    public InventoryChangedEvent inventoryChangedEvent;
    public ItemModel[] initialItems;
    private Dictionary<ItemModel, int> items;
    private UIAudioPlayer player;

    private void Awake()
    {
        player = GetComponent<UIAudioPlayer>();
        items = new Dictionary<ItemModel, int>();
        foreach (ItemModel item in initialItems)
        {
            AddItem(item);
        }
    }

    public void AddItem(ItemModel item)
    {
        if (!items.ContainsKey(item)) {
            items.Add(item, 1);
            inventoryChangedEvent.RaiseAdded(item);
            return;
        }
        items[item]++;
        inventoryChangedEvent.RaiseChanged(item, items[item]);
    }

    public void RemoveItem(ItemModel item)
    {
        items[item]--;
        if (items[item] == 0) {
            items.Remove(item);
            inventoryChangedEvent.RaiseRemoved(item);
            return;
        }
        inventoryChangedEvent.RaiseChanged(item, items[item]);
    }

    public void AddMoney(int quantity)
    {
        money += quantity;
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
        moneyChangedEvent.Raise(money);
        player.Play(0);
        return true;
    }

    public int GetMoney() {
        return money;
    }

    public Dictionary<ItemModel, int> GetItems()
    {
        return items;
    }
}
