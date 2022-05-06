using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FoodUI : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerExitHandler, PetBornEventListener
{
    public PetBornEvent petBornEvent;
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
        petBornEvent.petBornEvent += OnPetBorn;
        gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseHeld = true;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        mouseHeld = false;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (mouseHeld) InstantiateFood();
        mouseHeld = false;
    }

    void InstantiateFood()
    {
        GameObject newFood = Instantiate(foodPrototype, MathUtils.GetXZPlaneIntersection(Input.mousePosition, .5f, Camera.main), Quaternion.identity);
        newFood.GetComponent<Rigidbody>().isKinematic = false;
        newFood.GetComponent<FoodController>().foodModel = models[currentIndex];
        newFood.GetComponent<MouseFollower>().enabled = true;
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

    private void OnDestroy() {
        petBornEvent.petBornEvent -= OnPetBorn;
    }

    public void OnPetBorn(PetController pet) {
        gameObject.SetActive(true);
    }

}
