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

    private void Start() {
        mainPanel = transform.Find("MainPanel").GetComponent<RectTransform>();
        infoPanel = transform.Find("InfoPanel").GetComponent<RectTransform>();
        scaler = GetComponent<CanvasScaler>();
        mainPanel.anchoredPosition = new Vector2(0, mainPanel.anchoredPosition.y);
        infoPanel.anchoredPosition = new Vector2(scaler.referenceResolution.x, infoPanel.anchoredPosition.y);
    }

    public void SwapPanels() {
        uiAudioEvent.Raise(slideSound);
        StopAllCoroutines();
        if (showingMainPanel) StartCoroutine("HideMainPanel");
        else StartCoroutine("ShowMainPanel");
    }

    private IEnumerator ShowMainPanel() {
        showingMainPanel = true;
        while (mainPanel.anchoredPosition.x < -0.01 || infoPanel.anchoredPosition.x < scaler.referenceResolution.x - 0.01f) {
            Vector2 newPosition = mainPanel.anchoredPosition;
            newPosition.x = Mathf.Lerp(mainPanel.anchoredPosition.x, 0, 0.1f);
            mainPanel.anchoredPosition = newPosition;
            newPosition = infoPanel.anchoredPosition;
            newPosition.x = Mathf.Lerp(infoPanel.anchoredPosition.x, scaler.referenceResolution.x, 0.1f);
            infoPanel.anchoredPosition = newPosition;
            yield return null;
        }
    }

    private IEnumerator HideMainPanel() {
        showingMainPanel = false;
        while (mainPanel.anchoredPosition.x > -scaler.referenceResolution.x || infoPanel.anchoredPosition.x > 0f) {
            Vector2 newPosition = mainPanel.anchoredPosition;
            newPosition.x = Mathf.Lerp(mainPanel.anchoredPosition.x, -scaler.referenceResolution.x, 0.1f);
            mainPanel.anchoredPosition = newPosition;
            newPosition = infoPanel.anchoredPosition;
            newPosition.x = Mathf.Lerp(infoPanel.anchoredPosition.x, 0, 0.1f);
            infoPanel.anchoredPosition = newPosition;
            yield return null;
        }
    }
}
