using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public Color standardColor;
    public Color pressedColor;
    public Color selectedColor;
    public List<GameObject> objectsToSwap;
    List<Tab> buttons;
    Tab selected;
    TabAudioManager audioManager;

    private void Awake()
    {
        buttons = new List<Tab>();
        audioManager = GetComponent<TabAudioManager>();
    }

    public void Subscribe(Tab tab)
    {
        buttons.Add(tab);
    }

    public void OnClick(Tab tab)
    {
        if (selected != null && tab == selected) return;
        ResetTabs();
        tab.background.color = pressedColor;
        audioManager.PlayClick();
    }

    public void OnRelease(Tab tab)
    {
        if (selected != null && tab == selected) return;
        selected = tab;
        ResetTabs();
        tab.background.color = selectedColor;
        audioManager.PlayRelease();
        int index = tab.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++) {
            if (i == index) {
                objectsToSwap[i].SetActive(true);
            } else {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    private void ResetTabs()
    {
        foreach (Tab tab in buttons) {
            if (selected != null && tab == selected) continue;
            tab.background.color = standardColor;
        }
    }
}
