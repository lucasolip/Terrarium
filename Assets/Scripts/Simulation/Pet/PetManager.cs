using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    public PetBornEvent petBornEvent;
    public Vector3 startPosition;
    public GameObject petGameobject;
    public GameObject eggGameobject;
    List<PetController> pets;
    List<GameObject> eggs;

    void Start()
    {
        pets = new List<PetController>();
        eggs = new List<GameObject>();
        PetData data = SaveSystem.LoadPet();
        if (data != null) 
            CreatePet("", data);
        else 
            eggs.Add(Instantiate(eggGameobject, startPosition, Quaternion.identity, transform));
    }

    public void ResetPet() {
        SaveSystem.ResetPet();
        foreach (PetController pet in pets) Destroy(pet.gameObject);
        foreach (GameObject egg in eggs) Destroy(egg);
        Instantiate(eggGameobject, startPosition, Quaternion.identity, transform);
    }

    public void CreatePet(string petName, PetData data) {
        GameObject pet = Instantiate(petGameobject, transform.position, Quaternion.identity, transform.parent);
        PetController petController = pet.GetComponent<PetController>();
        pets.Add(petController);
        petController.petName = petName;
        if (data != null) data.LoadPet(petController);
        //GameObject.Find("CameraTarget").GetComponent<CameraTargetController>().target = pet.transform;
        petBornEvent.Raise(petController);
    }

}
