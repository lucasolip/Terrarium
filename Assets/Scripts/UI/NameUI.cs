using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NameUI : MonoBehaviour
{
    Text title;
    Text input;
    UIAnimator animator;
    public event Action inputSent;
    private void Awake() {
        title = transform.Find("TitlePanel").Find("Title").GetComponent<Text>();
        input = transform.Find("Text").GetComponent<Text>();
        animator = GetComponent<UIAnimator>();
    }

    public void Show() {
        animator.ScaleIn(gameObject);
    }

    public void InputSent() {
        if (inputSent != null) inputSent();
    }

    public string Dispose() {
        string result = input.text;
        input.text = "";
        animator.ScaleOut(gameObject);
        return result;
    }

    public void SetTitle(string text)
    {
        title.text = text;
    }
}