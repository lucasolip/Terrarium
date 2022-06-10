using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NameUI : MonoBehaviour
{
    Text input;
    UIAnimator animator;
    public event Action inputSent;
    private void Awake() {
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
        animator.ScaleOut(gameObject);
        return input.text;
    }
}