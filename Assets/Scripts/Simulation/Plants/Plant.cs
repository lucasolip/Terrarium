using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Plant : MonoBehaviour, TickEventListener
{
    public TickEvent tickEvent;
    public FertileBlock terrainBlock;

    public abstract void Chop();

    public abstract void OnTick();

    private void OnEnable() {
        tickEvent.tickEvent += OnTick;
    }

    private void OnDisable() {
        tickEvent.tickEvent -= OnTick;
    }
}
