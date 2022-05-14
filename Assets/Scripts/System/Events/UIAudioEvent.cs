using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Event/UIAudio")]
public class UIAudioEvent : ScriptableObject
{
    public event Action<AudioClip> uiAudioEvent;
    private List<UIAudioEventListener> listeners = new List<UIAudioEventListener>();

    public void Raise(AudioClip clip)
    {
        if (uiAudioEvent != null) uiAudioEvent(clip);
    }
}
