using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MouseHandler
{
    public PetBornEvent petBornEvent;
    public GameObject petGameobject;
    public Transform cameraTarget;
    public NameUI inputField;

    private void Start() {
        PetData data = SaveSystem.LoadPet();
        if (data != null) CreatePet("", data);
        cameraTarget.position = transform.position;
        inputField.inputSent += OnInputSent;
    }

    private void OnInputSent() {
        string petName = inputField.Dispose();
        CreatePet(petName, null);
    }

    private void CreatePet(string petName, PetData data) {
        GameObject pet = Instantiate(petGameobject, transform.position, Quaternion.identity);
        cameraTarget.position = pet.transform.position;
        PetController petController = pet.GetComponent<PetController>();
        petController.petName = petName;
        if (data != null) data.LoadPet(petController);
        //GameObject.Find("CameraTarget").GetComponent<CameraTargetController>().target = pet.transform;
        petBornEvent.Raise(petController);
        Destroy(gameObject);
    }

    public override void Clicked()
    {
        inputField.Show();
    }

    public override void Dragged()
    {
    }

    public override void Released()
    {
        
    }
}
