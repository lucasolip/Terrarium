using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip[] clips;

    public void Play(int index)
    {
        AudioSource.PlayClipAtPoint(clips[index], transform.position);
    }
}
