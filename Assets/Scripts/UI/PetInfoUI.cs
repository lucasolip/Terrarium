using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PetInfoUI : MonoBehaviour, PetBornEventListener
{
    public PetBornEvent petBornEvent;
    private PetController pet;
    private Text petName;
    private Text petAge;
    private Text petStage;
    private DateTime dateBorn;

    private void Awake() {
        petName = transform.Find("Info").Find("Name").GetComponent<Text>();
        petAge = transform.Find("Info").Find("Age").GetComponent<Text>();
        petStage = transform.Find("Info").Find("Stage").GetComponent<Text>();
        petBornEvent.petBornEvent += OnPetBorn;
        gameObject.SetActive(false);
    }

    private void Update() {
        petAge.text = "Edad: " + TimeFormatter.Format(DateTime.Now - dateBorn);
        petStage.text = "Etapa: " + pet.stage.stageName;
    }

    public void OnPetBorn(PetController pet) {
        this.pet = pet;
        petName.text = pet.petName;
        dateBorn = pet.bornTime;
        gameObject.SetActive(true);
    }

    private void OnDestroy() {
        petBornEvent.petBornEvent -= OnPetBorn;
    }
}
