using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioPlayer : MonoBehaviour
{
    public UIAudioEvent audioEvent;
    public AudioClip[] clip;

    public void Play(int index)
    {
        audioEvent.Raise(clip[index]);
    }
}
