using UnityEngine;

public class Clock : MonoBehaviour
{
    public TickEvent tickEvent;
    public int tickPeriod = 3;
    public static int _tickPeriod;
    private void Awake()
    {
        _tickPeriod = tickPeriod;
    }

    void FixedUpdate()
    {
        if (Time.time % tickPeriod == 0)
        {
            tickEvent.Raise();
        }
    }
}
