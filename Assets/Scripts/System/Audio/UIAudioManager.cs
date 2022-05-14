using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioManager : MonoBehaviour, UIAudioEventListener
{
    public UIAudioEvent uiAudioEvent;
    AudioSource source;

    private void Start() {
        source = GetComponent<AudioSource>();
    }

    public void OnUISound(AudioClip clip) {
        source.PlayOneShot(clip);
    }

    private void OnEnable() {
        uiAudioEvent.uiAudioEvent += OnUISound;
    }

    private void OnDisable() {
        uiAudioEvent.uiAudioEvent -= OnUISound;
    }
}
