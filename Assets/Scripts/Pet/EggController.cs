using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{
    public GameObject petGameobject;
    public Transform cameraTarget;
    public NameUI inputField;

    private void Start() {
        cameraTarget.position = transform.position;
        inputField.inputSent += OnInputSent;
    }

    private void OnMouseDown() {
        inputField.Show();
    }

    private void OnInputSent() {
        string petName = inputField.Dispose();
        GameObject pet = Instantiate(petGameobject, transform.position, Quaternion.identity);
        cameraTarget.position = pet.transform.position;
        pet.GetComponent<PetController>().petName = petName;
        Destroy(gameObject);
    }
}
