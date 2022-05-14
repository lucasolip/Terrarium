using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ToolPanelUI : MonoBehaviour
{
    public MouseController mouseController;
    RectTransform toolBar;
    RectTransform selectedTool;
    float targetPosition;
    Image mainIcon;
    bool shown = false;
    private ShowUIAudioManager showAudioManager;
    private UIAnimator animator;
    void Start()
    {
        mainIcon = transform.Find("ShowButton").GetComponent<Image>();
        toolBar = transform.Find("Tools").GetComponent<RectTransform>();
        toolBar.localScale = new Vector3(0, toolBar.localScale.y, toolBar.localScale.z);
        selectedTool = toolBar.Find("Selected").GetComponent<RectTransform>();
        targetPosition = selectedTool.anchoredPosition.x;
        if (mouseController == null) mouseController = GameObject.Find("Canvas").GetComponent<MouseController>();
        showAudioManager = GetComponent<ShowUIAudioManager>();
        animator = GetComponent<UIAnimator>();
    }

    public void Show()
    {
        if (!shown) ShowUI();
        else HideUI();
    }

    public void Selected(GameObject toolButton)
    {
        mainIcon.sprite = toolButton.transform.Find("Icon").GetComponent<Image>().sprite;
        Vector3 toolPosition = toolButton.GetComponent<RectTransform>().anchoredPosition;
        toolPosition.x -= 22;
        animator.Move(selectedTool, toolPosition);
        mouseController.SetTool(toolButton.GetComponent<ToolController>());
    }

    private void HideUI()
    {
        showAudioManager.HideSound();
        shown = false;
        animator.ScaleOut(toolBar.gameObject, HideToolbar);
    }

    private void ShowUI()
    {
        showAudioManager.ShowSound();
        shown = true;
        toolBar.gameObject.SetActive(true);
        animator.ScaleIn(toolBar.gameObject);
    }

    private void HideToolbar()
    {
        toolBar.gameObject.SetActive(false);
    }

}
