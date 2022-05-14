using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUI : MonoBehaviour
{
    bool showingMainPanel = true;
    RectTransform mainPanel;
    RectTransform infoPanel;
    CanvasScaler scaler;
    public UIAudioEvent uiAudioEvent;
    public AudioClip slideSound;
    private UIAnimator animator;

    private void Start() {
        mainPanel = transform.Find("MainPanel").GetComponent<RectTransform>();
        infoPanel = transform.Find("InfoPanel").GetComponent<RectTransform>();
        scaler = GetComponent<CanvasScaler>();
        animator = GetComponent<UIAnimator>();
        mainPanel.anchoredPosition = new Vector2(0, mainPanel.anchoredPosition.y);
        infoPanel.anchoredPosition = new Vector2(scaler.referenceResolution.x, infoPanel.anchoredPosition.y);
    }

    public void SwapPanels() {
        uiAudioEvent.Raise(slideSound);
        StopAllCoroutines();
        if (showingMainPanel) HideMainPanel();
        else ShowMainPanel();
    }

    private void ShowMainPanel() {
        showingMainPanel = true;
        animator.Move(mainPanel, new Vector3(0, infoPanel.anchoredPosition.y, 0));
        animator.Move(infoPanel, new Vector3(scaler.referenceResolution.x, infoPanel.anchoredPosition.y, 0));
    }

    private void HideMainPanel() {
        showingMainPanel = false;
        animator.Move(mainPanel, new Vector3(-scaler.referenceResolution.x, mainPanel.anchoredPosition.y, 0));
        animator.Move(infoPanel, new Vector3(0, infoPanel.anchoredPosition.y, 0));
    }
}
