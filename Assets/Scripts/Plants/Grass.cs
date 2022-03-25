using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour, TickEventListener, Plant
{
    public GameObject grassGameobject;
    public TickEvent tickEvent;
    public Mesh shortGrassMesh;
    public Mesh tallGrassMesh;
    public int reproduceAge;
    public int growAge;
    [HideInInspector]
    public FertileBlock terrainBlock;
    bool isTall = false;
    int age = 0;
    MeshFilter meshFilter;
    Collider collider;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = shortGrassMesh;
        collider = GetComponent<Collider>();
    }

    public void OnTick()
    {
        Debug.Log("Grass tick");
        age++;
        if (!isTall && age > growAge)
        {
            isTall = true;
            collider.enabled = true;
            meshFilter.mesh = tallGrassMesh;
        }
        if (age > reproduceAge)
        {
            Reproduce();
        }
    }

    void Reproduce()
    {
        GameObject newObject = Instantiate(grassGameobject);
        Plant newGrass = newObject.GetComponent<Grass>();
        terrainBlock.PlantNeighbour(newGrass);
    }

    private void OnEnable()
    {
        tickEvent.tickEvent += OnTick;
    }

    private void OnDisable()
    {
        tickEvent.tickEvent -= OnTick;
    }
}
