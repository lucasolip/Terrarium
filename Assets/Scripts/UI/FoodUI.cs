using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FoodUI : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerExitHandler
{
    public FoodModel[] models;
    public GameObject foodPrototype;
    int currentIndex = 0;
    Image foodIcon;
    bool mouseHeld = false;
    CameraController cam;

    private void Awake()
    {
        foodIcon = transform.GetChild(0).GetComponent<Image>();
        SetIcon(models[currentIndex].model);
    }

    private void Start() {
        cam = GameObject.Find("VirtualCamera").GetComponent<CameraController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseHeld = true;
        cam.Freeze();
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        mouseHeld = false;
        cam.Unfreeze();
    }

    void InstantiateFood()
    {
        GameObject newFood = Instantiate(foodPrototype, MathUtils.GetXZPlaneIntersection(Input.mousePosition, .5f, Camera.main), Quaternion.identity);
        newFood.GetComponent<FoodController>().foodModel = models[currentIndex];
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
        currentIndex = (currentIndex + 1) % models.Length;
        SetIcon(models[currentIndex].model);
    }

    public void PreviousElement()
    {
        currentIndex = Mathf.Abs(currentIndex - 1) % models.Length;
        SetIcon(models[currentIndex].model);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (mouseHeld)
        {
            InstantiateFood();
        }
        mouseHeld = false;
    }
}
