using UnityEngine;

public abstract class TickEventListener : MonoBehaviour
{
    public TickEvent tickEvent;
    public abstract void OnTick();

    private void OnEnable()
    {
        tickEvent.tickEvent += OnTick;
    }

    private void OnDisable()
    {
        tickEvent.tickEvent -= OnTick;
    }
}
