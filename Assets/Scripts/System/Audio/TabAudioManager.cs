using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabAudioManager : MonoBehaviour
{
    public AudioClip downSound;
    public AudioClip upSound;
    public UIAudioEvent uiAudioEvent;

    public void PlayClick()
    {
        uiAudioEvent.Raise(downSound);
    }

    public void PlayRelease()
    {
        uiAudioEvent.Raise(upSound);
    }
}
