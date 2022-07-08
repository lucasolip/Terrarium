using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemUI))]
[RequireComponent(typeof(UIAudioPlayer))]
public class ItemPickerUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public MouseController mouseController;
    public GameObject itemPrototype;
    ItemUI itemUI;
    UIAudioPlayer player;
    bool mouseHeld = false;

    private void Start()
    {
        itemUI = GetComponent<ItemUI>();
        player = GetComponent<UIAudioPlayer>();
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
        if (item != null)
        {
            itemUI.userInventory.AddItem(item.model);
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
        newFood.GetComponent<ItemController>().model = itemUI.items[itemUI.currentIndex];
        newFood.GetComponent<MouseFollower>().enabled = true;
        itemUI.PickItem();
    }
}
