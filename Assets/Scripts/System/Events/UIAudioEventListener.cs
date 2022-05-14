using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UIAudioEventListener
{
    public abstract void OnUISound(AudioClip clip);
}
