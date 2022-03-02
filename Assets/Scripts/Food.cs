using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public FoodModel foodModel;

    private void Start()
    {
        GetComponent<Renderer>().material.mainTexture = foodModel.model;
    }
}
