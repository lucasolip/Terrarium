using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemElementUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ItemListUI itemList;
    public ItemModel item;
    RawImage icon;
    Image background;
    Text quantity;

    private void Awake()
    {
        background = GetComponent<Image>();
        icon = transform.GetChild(0).GetComponent<RawImage>();
        quantity = transform.Find("QuantityPanel").Find("Quantity").GetComponent<Text>();
    }

    public void SetIcon(Texture texture)
    {
        icon.texture = texture;
    }

    public void SetBackgroundColor(Color color)
    {
        background.color = color;
    }

    public void SetQuantity(int quantity)
    {
        this.quantity.text = quantity.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        itemList.Clicked(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        itemList.Released(this);
    }
}
