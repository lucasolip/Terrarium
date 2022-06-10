using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsUI : MonoBehaviour
{
    UIAnimator animator;
    ShowUIAudioManager showAudio;

    private void Awake() {
        animator = GetComponent<UIAnimator>();
        showAudio = GetComponent<ShowUIAudioManager>();
    }

    public void Show() {
        gameObject.SetActive(true);
        animator.ScaleIn(gameObject);
        showAudio.ShowSound();
    }

    public void Hide() {
        animator.ScaleOut(gameObject, () => gameObject.SetActive(false));
        showAudio.HideSound();
    }
}
