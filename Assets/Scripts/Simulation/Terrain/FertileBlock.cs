using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertileBlock : TerrainBlock, TickEventListener
{
    public TickEvent tickEvent;
    public bool wet = true;
    public Grass grass;
    public TreeModel tree;

    private void Start()
    {
        tickEvent.tickEvent += OnTick;
    }

    public override void Water() {
        wet = true;
    }

    public bool Plant(GameObject plantPrefab)
    {
        return terrain.Plant(plantPrefab, this);
    }

    public void PlantNeighbour(GameObject grassGameObject)
    {
        Quaternion rotation = (terrain.rotateGrass) ? Quaternion.Euler(0, Random.Range(0f, 360f), 0) : Quaternion.identity;
        Grass seed = Instantiate(grassGameObject, grassGameObject.transform.position,
                rotation).GetComponent<Grass>();
        int randomX = Random.Range(-1, 2);
        int randomY = Random.Range(-1, 2);
        if (randomX == 0 && randomY == 0) {
            Destroy(seed.gameObject);
            return;
        }
        terrain.Plant(x + randomX, y + randomY, seed);
    }

    public void OnTick()
    {
        // TODO: After some number of ticks, wet blocks get dry
        Debug.Log("Fertile block tick");
    }

    private void OnDestroy()
    {
        tickEvent.tickEvent -= OnTick;
    }
}
