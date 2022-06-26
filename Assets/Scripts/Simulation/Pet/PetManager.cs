using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour, PetDiedEventListener
{
    public PetBornEvent petBornEvent;
    public PetDiedEvent petDiedEvent;
    public Vector3 startPosition;
    public GameObject petGameobject;
    public GameObject eggGameobject;
    public GameObject deadGameobject;
    List<PetController> pets;
    List<GameObject> eggs;

    void Awake()
    {
        pets = new List<PetController>();
        eggs = new List<GameObject>();
    }

    private void Start()
    {
        PetData data = SaveSystem.LoadPet();
        if (data != null) 
            CreatePet("", data);
        else 
            eggs.Add(Instantiate(eggGameobject, startPosition, Quaternion.identity, transform));
        petDiedEvent.petDiedEvent += OnPetDied;
    }

    public void ResetPet() {
        SaveSystem.ResetPet();
        foreach (PetController pet in pets) Destroy(pet.gameObject);
        foreach (GameObject egg in eggs) Destroy(egg);
        pets.Clear();
        eggs.Clear();
        for (int i = 0; i < transform.childCount; i++) Destroy(transform.GetChild(i).gameObject);
        eggs.Add(Instantiate(eggGameobject, startPosition, Quaternion.identity, transform));
    }

    public void CreatePet(string petName, PetData data) {
        GameObject pet = Instantiate(petGameobject, transform.position, Quaternion.identity, transform);
        PetController petController = pet.GetComponent<PetController>();
        if (data != null) {
            if (data.IsDead()) {
                Destroy(pet);
                Instantiate(deadGameobject, transform);
                return;
            }
            data.LoadPet(petController);
        } else {
            petController.petName = petName;
            petController.bornTime = System.DateTime.Now;
        }
        pets.Add(petController);
        GameObject.Find("CameraTarget").GetComponent<CameraTargetController>().target = pet.transform;
        petBornEvent.Raise(petController);
    }

    public void OnPetDied(PetController pet)
    {
        pets.Remove(pet);
        Destroy(pet.gameObject);
        Instantiate(deadGameobject, pet.transform.position, Quaternion.identity, transform);
    }
}
