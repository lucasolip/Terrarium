using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUIAudioManager : MonoBehaviour
{
    public AudioClip showSound;
    public AudioClip hideSound;
    public UIAudioEvent uiAudioEvent;

    public void ShowSound() {
        uiAudioEvent.Raise(showSound);
    }

    public void HideSound() {
        uiAudioEvent.Raise(hideSound);
    }
}
