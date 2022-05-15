using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Plant
{
    public GameObject grassGameobject;
    public Vector3 shortScale;
    public Vector3 tallScale;
    public int reproduceAge;
    public int growAge;

    bool isTall = false;
    int age = 0;
    Collider objectCollider;

    private void Awake()
    {
        objectCollider = GetComponent<Collider>();
        transform.localScale = shortScale;
    }

    public override void OnTick()
    {
        Debug.Log("Grass tick");
        age++;
        if (!isTall && age > growAge)
        {
            isTall = true;
            objectCollider.enabled = true;
            transform.localScale = tallScale;
        }
        if (age > reproduceAge)
        {
            Reproduce();
        }
    }

    void Reproduce()
    {
        terrainBlock.PlantNeighbour(grassGameobject);
    }

    public override void Chop()
    {
        terrainBlock.grass = null;
        Destroy(gameObject);
    }
}
