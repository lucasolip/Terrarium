using UnityEngine.UI;
using System;

public class TimeUI : TickEventListener
{
    Text text;
    private void Start() {
        text = GetComponent<Text>();
    }

    public override void OnTick() {
        text.text = DateTime.Now.ToString("HH:mm");
    }
}
