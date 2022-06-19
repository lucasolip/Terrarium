using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScriptableObjectLocator : MonoBehaviour
{
    public ScriptableObject[] objects;
    private static List<ScriptableObject> objectList;

    private void Awake() {
        objectList = new List<ScriptableObject>();
        foreach (ScriptableObject obj in objects) {
            objectList.Add(obj);
        }
    }

    public static int GetIndex(ScriptableObject obj) {
        try {
            return objectList.IndexOf(obj);
        } catch (ArgumentOutOfRangeException argException) {
            Debug.LogError("Missing ScriptableObject from ScriptableObjectLocator");
            return -1;
        }
    }

    public static ScriptableObject Get(int index) {
        try {
            return objectList[index];
        } catch (ArgumentOutOfRangeException argException) {
            Debug.LogError("Missing ScriptableObject from ScriptableObjectLocator. " + argException.StackTrace);
            return null;
        }
    }
}
