using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, 
    IPointerExitHandler, PetBornEventListener, InventoryChangedEventListener, IPointerEnterHandler, PetDiedEventListener
{
    public MouseController mouseController;
    public InventoryChangedEvent inventoryChanged;
    public PetBornEvent petBornEvent;
    public PetDiedEvent petDiedEvent;
    public GameObject itemPrototype;
    public InventoryController userInventory;
    private UIAudioPlayer player;
    private List<ItemModel> items;
    private List<int> itemQuantities;
    int currentIndex = 0;
    Image foodIcon;
    TextMeshProUGUI quantity;
    bool mouseHeld = false;
    bool petBorn = false;

    private void Awake()
    {
        foodIcon = transform.Find("FoodIcon").GetComponent<Image>();
        quantity = transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
        player = GetComponent<UIAudioPlayer>();
        items = new List<ItemModel>();
        itemQuantities = new List<int>();
        petBornEvent.petBornEvent += OnPetBorn;
        petDiedEvent.petDiedEvent += OnPetDied;
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
        if (!petBorn) gameObject.SetActive(false);
    }

    public ItemModel GetCurrentItem()
    {
        return items[currentIndex];
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseHeld = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.pointerPress = gameObject;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mouseHeld = false;
        MouseHandler handler = mouseController.selected;
        ItemController item = null;
        if (handler != null) item = handler.transform.GetComponent<ItemController>();
        if (item != null) {
            userInventory.AddItem(item.model);
            player.Play(0);
            Destroy(item.gameObject);
            mouseController.selected = null;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.pointerPress = null;
        if (mouseHeld) InstantiateItem();
        mouseHeld = false;
    }

    void InstantiateItem()
    {
        GameObject newFood = Instantiate(itemPrototype, MathUtils.GetXZPlaneIntersection(Input.mousePosition, .5f, Camera.main), Quaternion.identity);
        newFood.GetComponent<Rigidbody>().isKinematic = false;
        newFood.GetComponent<ItemController>().model = items[currentIndex];
        newFood.GetComponent<MouseFollower>().enabled = true;
        userInventory.RemoveItem(items[currentIndex]);
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
        petBornEvent.petBornEvent -= OnPetBorn;
        petDiedEvent.petDiedEvent -= OnPetDied;
        inventoryChanged.itemAdded -= OnItemAdded;
        inventoryChanged.itemChanged -= OnItemChanged;
        inventoryChanged.itemRemoved -= OnItemRemoved;
    }

    public void OnPetBorn(PetController pet) {
        petBorn = true;
        gameObject.SetActive(true);
    }

    public void OnPetDied(PetController pet)
    {
        petBorn = false;
        gameObject.SetActive(false);
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
