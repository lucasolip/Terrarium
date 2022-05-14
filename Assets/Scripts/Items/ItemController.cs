using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public ItemModel model;

    private void Start()
    {
        GetComponent<Renderer>().material.mainTexture = model.model;
    }
}
