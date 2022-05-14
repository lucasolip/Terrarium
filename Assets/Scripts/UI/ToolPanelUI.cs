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
    void Start()
    {
        mainIcon = transform.Find("ShowButton").GetComponent<Image>();
        toolBar = transform.Find("Tools").GetComponent<RectTransform>();
        toolBar.localScale = new Vector3(0, toolBar.localScale.y, toolBar.localScale.z);
        selectedTool = toolBar.Find("Selected").GetComponent<RectTransform>();
        targetPosition = selectedTool.anchoredPosition.x;
        if (mouseController == null) mouseController = GameObject.Find("Canvas").GetComponent<MouseController>();
        showAudioManager = GetComponent<ShowUIAudioManager>();
    }

    private void Update()
    {
        if (Mathf.Abs(targetPosition - selectedTool.anchoredPosition.x) > 0.01f) {
            Vector2 newPosition = selectedTool.anchoredPosition;
            newPosition.x = Mathf.Lerp(selectedTool.anchoredPosition.x, targetPosition, 0.1f);
            selectedTool.anchoredPosition = newPosition;
        }
    }

    public void Show()
    {
        StopAllCoroutines();
        if (!shown) StartCoroutine("ShowUI");
        else StartCoroutine("HideUI");
    }

    public void Selected(GameObject toolButton)
    {
        mainIcon.sprite = toolButton.transform.Find("Icon").GetComponent<Image>().sprite;
        targetPosition = toolButton.GetComponent<RectTransform>().anchoredPosition.x - 22;
        mouseController.SetTool(toolButton.GetComponent<ToolController>());
    }

    private IEnumerator HideUI()
    {
        showAudioManager.HideSound();
        shown = false;
        while (toolBar.localScale.x > 0.01) {
            Vector3 scale = toolBar.localScale;
            scale.x = Mathf.Lerp(toolBar.localScale.x, 0, 0.1f);
            toolBar.localScale = scale;
            yield return null;
        }
        toolBar.gameObject.SetActive(false);
    }

    private IEnumerator ShowUI()
    {
        showAudioManager.ShowSound();
        shown = true;
        toolBar.gameObject.SetActive(true);
        while (toolBar.localScale.x < 0.99) {
            Vector3 scale = toolBar.localScale;
            scale.x = Mathf.Lerp(toolBar.localScale.x, 1, 0.1f);
            toolBar.localScale = scale;
            yield return null;
        }
    }

}
