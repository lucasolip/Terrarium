using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Plant
{
    public GameObject grassGameobject;
    public Mesh shortGrassMesh;
    public Mesh tallGrassMesh;
    public int reproduceAge;
    public int growAge;

    bool isTall = false;
    int age = 0;
    MeshFilter meshFilter;
    Collider objectCollider;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = shortGrassMesh;
        objectCollider = GetComponent<Collider>();
    }

    public override void OnTick()
    {
        Debug.Log("Grass tick");
        age++;
        if (!isTall && age > growAge)
        {
            isTall = true;
            objectCollider.enabled = true;
            meshFilter.mesh = tallGrassMesh;
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
}
