using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MouseHandler
{
    public GameObject petGameobject;
    public Transform cameraTarget;
    public NameUI inputField;

    private void Start() {
        cameraTarget.position = transform.position;
        inputField.inputSent += OnInputSent;
    }

    private void OnInputSent() {
        string petName = inputField.Dispose();
        GameObject pet = Instantiate(petGameobject, transform.position, Quaternion.identity);
        cameraTarget.position = pet.transform.position;
        pet.GetComponent<PetController>().petName = petName;
        //GameObject.Find("CameraTarget").GetComponent<CameraTargetController>().target = pet.transform;
        Transform canvas = GameObject.Find("Canvas").transform;
        canvas.GetChild(0).gameObject.SetActive(true);
        canvas.GetChild(1).gameObject.SetActive(true);
        Destroy(gameObject);
    }

    public override void Clicked()
    {
        inputField.Show();
    }

    public override void Released()
    {
        
    }
}
