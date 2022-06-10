using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUI : MonoBehaviour
{
    int index = 0;
    RectTransform[] panels;
    CanvasScaler scaler;
    public UIAudioEvent uiAudioEvent;
    public AudioClip slideSound;
    private UIAnimator animator;

    private void Start() {
        scaler = GetComponent<CanvasScaler>();
        animator = GetComponent<UIAnimator>();
        panels = new RectTransform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            panels[i] = transform.GetChild(i).GetComponent<RectTransform>();
            panels[i].anchoredPosition = new Vector2(scaler.referenceResolution.x, panels[i].anchoredPosition.y);
        }
        panels[0].anchoredPosition = new Vector2(0, 0);
    }

    public void Next() {
        uiAudioEvent.Raise(slideSound);
        RectTransform currentPanel = panels[index];
        RectTransform nextPanel = panels[index + 1];
        animator.Move(currentPanel, new Vector3(-scaler.referenceResolution.x, currentPanel.anchoredPosition.y, 0));
        animator.Move(nextPanel, new Vector3(0, nextPanel.anchoredPosition.y, 0));
        index++;
    }

    public void Previous() {
        uiAudioEvent.Raise(slideSound);
        RectTransform currentPanel = panels[index];
        RectTransform previousPanel = panels[index - 1];
        animator.Move(currentPanel, new Vector3(scaler.referenceResolution.x, currentPanel.anchoredPosition.y, 0));
        animator.Move(previousPanel, new Vector3(0, previousPanel.anchoredPosition.y, 0));
        index--;
    }
}
