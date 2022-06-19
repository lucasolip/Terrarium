using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MouseHandler
{
    public NameUI inputField;

    private void Start() {
        inputField = GameObject.Find("NameField").GetComponent<NameUI>();
        inputField.inputSent += OnInputSent;
    }

    private void OnInputSent() {
        string petName = inputField.Dispose();
        transform.parent.GetComponent<PetManager>().CreatePet(petName, null);
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

    private void OnDestroy()
    {
        inputField.inputSent -= OnInputSent;
    }
}
