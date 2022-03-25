using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertileBlock : MonoBehaviour, TerrainBlock, TickEventListener
{
    public TickEvent tickEvent;
    public bool wet = true;
    public TerrainController terrain;
    public int x, y;
    public Grass grass;

    public void Water() {
        wet = true;
    }

    public void PlantNeighbour(Plant seed)
    {
        int randomX = 2 * Random.Range(0, 1) - 1;
        int randomY = 2 * Random.Range(0, 1) - 1;
        terrain.Plant(x + randomX, y + randomY, seed);
    }

    public void OnTick()
    {
        // TODO: After some number of ticks, wet blocks get dry
        Debug.Log("Fertile block tick");
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
