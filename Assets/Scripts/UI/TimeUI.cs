using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeUI : MonoBehaviour, TickEventListener
{
    public TickEvent tickEvent;
    Text text;
    private void Start() {
        text = GetComponent<Text>();
    }

    public void OnTick() {
        text.text = DateTime.Now.ToString("HH:mm");
    }

    private void OnEnable() {
        tickEvent.tickEvent += OnTick;
    }

    private void OnDisable() {
        tickEvent.tickEvent -= OnTick;
    }
}
