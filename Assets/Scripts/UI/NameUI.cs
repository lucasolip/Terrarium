using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NameUI : MonoBehaviour
{
    Text input;
    public event Action inputSent;
    private void Awake() {
        input = transform.Find("Text").GetComponent<Text>();
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void InputSent() {
        if (inputSent != null) inputSent();
    }

    public string Dispose() {
        gameObject.SetActive(false);
        return input.text;
    }
}