using UnityEngine;

public class Clock : MonoBehaviour
{
    public TickEvent tickEvent;
    public int tickPeriod = 3;

    void FixedUpdate()
    {
        if (Time.time % tickPeriod == 0)
        {
            tickEvent.Raise();
        }
    }
}
