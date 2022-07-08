using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PetInfoUI : MonoBehaviour, PetBornEventListener, PetDiedEventListener
{
    public PetBornEvent petBornEvent;
    public PetDiedEvent petDiedEvent;
    private PetController pet;
    private Text petName;
    private Text petAge;
    private Text petStage;
    private RawImage image;
    private DateTime dateBorn;

    private void Awake() {
        petName = transform.Find("Info").Find("Name").GetComponent<Text>();
        petAge = transform.Find("Info").Find("Age").GetComponent<Text>();
        petStage = transform.Find("Info").Find("Stage").GetComponent<Text>();
        image = transform.Find("Image").GetComponent<RawImage>();
        enabled = false;
        gameObject.SetActive(false);
        petBornEvent.petBornEvent += OnPetBorn;
        petDiedEvent.petDiedEvent += OnPetDied;
    }

    private void Update() {
        if (pet == null) {
            enabled = false;
            gameObject.SetActive(false);
        }
        petAge.text = "Edad: " + TimeFormatter.Format(DateTime.Now - dateBorn);
        petStage.text = "Etapa: " + pet.stage.stageName;
        image.texture = pet.stage.fineModel;
    }

    public void OnPetBorn(PetController pet) {
        this.pet = pet;
        petName.text = pet.petName;
        dateBorn = pet.bornTime;
        enabled = true;
        gameObject.SetActive(true);
    }

    public void OnPetDied(PetController pet)
    {
        enabled = false;
    }

    private void OnDestroy() {
        petBornEvent.petBornEvent -= OnPetBorn;
        petDiedEvent.petDiedEvent -= OnPetDied;
    }
}
