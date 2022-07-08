using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour, InventoryChangedEventListener
{
    public InventoryChangedEvent inventoryChanged;
    public InventoryController userInventory;
    [HideInInspector]
    public List<ItemModel> items;
    private List<int> itemQuantities;
    public int currentIndex = 0;
    Image foodIcon;
    TextMeshProUGUI quantity;

    private void Awake()
    {
        foodIcon = transform.Find("FoodIcon").GetComponent<Image>();
        quantity = transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
        items = new List<ItemModel>();
        itemQuantities = new List<int>();
        inventoryChanged.itemAdded += OnItemAdded;
        inventoryChanged.itemChanged += OnItemChanged;
        inventoryChanged.itemRemoved += OnItemRemoved;
    }

    private void Start()
    {
        if (items.Count == 0) {
            Dictionary<ItemModel, int> inventoryItems = userInventory.GetItems();
            foreach (KeyValuePair<ItemModel, int> pair in inventoryItems)
            {
                items.Add(pair.Key);
                itemQuantities.Add(pair.Value);
            }
            if (items.Count > 0) {
                SetIcon(items[0].model);
                quantity.text = itemQuantities[0].ToString();
            }
        }
    }

    public ItemModel GetCurrentItem()
    {
        return items[currentIndex];
    }

    void SetIcon(Texture icon)
    {
        Texture2D foodTexture = (Texture2D) icon;
        Sprite foodSprite = Sprite.Create(foodTexture, new Rect(0, 0, foodTexture.width, foodTexture.height), Vector2.one * .5f);
        foodIcon.sprite = foodSprite;
    }

    void SetIcon(Sprite icon)
    {
        foodIcon.sprite = icon;
    }

    public void PickItem()
    {
        userInventory.RemoveItem(items[currentIndex]);
    }

    public bool IsEmpty()
    {
        return items.Count == 0;
    }

    public void NextElement()
    {
        if (items.Count == 0) {
            SetIcon(TextureLocator.nullTexture);
            quantity.text = "";
            return;
        }
        currentIndex = (currentIndex + 1) % items.Count;
        SetIcon(items[currentIndex].model);
        quantity.text = itemQuantities[currentIndex].ToString();
    }

    public void PreviousElement()
    {
        if (items.Count == 0) {
            SetIcon(TextureLocator.nullTexture);
            quantity.text = "";
            return;
        }
        currentIndex = (currentIndex - 1) % items.Count;
        if (currentIndex < 0) currentIndex = items.Count - 1;
        SetIcon(items[currentIndex].model);
        quantity.text = itemQuantities[currentIndex].ToString();
    }

    private void OnDestroy() {
        inventoryChanged.itemAdded -= OnItemAdded;
        inventoryChanged.itemChanged -= OnItemChanged;
        inventoryChanged.itemRemoved -= OnItemRemoved;
    }

    public void OnItemAdded(ItemModel item)
    {
        items.Add(item);
        itemQuantities.Add(1);
        if (items.Count == 1) {
            SetIcon(items[0].model);
            quantity.text = itemQuantities[0].ToString();
        }
    }

    public void OnItemChanged(ItemModel item, int quantity)
    {
        int index = items.IndexOf(item);
        itemQuantities[index] = quantity;
        if (index == currentIndex) {
            this.quantity.text = itemQuantities[currentIndex].ToString();
        }
    }

    public void OnItemRemoved(ItemModel item)
    {
        int index = items.IndexOf(item);
        items.Remove(item);
        itemQuantities.RemoveAt(index);
        PreviousElement();
    }
}
