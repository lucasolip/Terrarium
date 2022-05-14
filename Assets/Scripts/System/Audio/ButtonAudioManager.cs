using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAudioManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public AudioClip downSound;
    public AudioClip upSound;
    public UIAudioEvent uiAudioEvent;

    public void OnPointerDown(PointerEventData pointerEventData) {
        uiAudioEvent.Raise(downSound);
    }

    public void OnPointerUp(PointerEventData pointerEventData) {
        uiAudioEvent.Raise(upSound);
    }
}
