using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MouseHandler
{
    public NameUI inputField;
    public string nameFieldTitle;

    private void Start() {
        inputField = GameObject.Find("NameField").GetComponent<NameUI>();
        
    }

    private void OnInputSent() {
        string petName = inputField.Dispose();
        transform.parent.GetComponent<PetManager>().CreatePet(petName, null);
        Destroy(gameObject);
    }    

    public override void Clicked()
    {
        inputField.inputSent += OnInputSent;
        inputField.SetTitle(nameFieldTitle);
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
