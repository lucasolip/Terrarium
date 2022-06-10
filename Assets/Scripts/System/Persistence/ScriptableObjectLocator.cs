using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        return objectList.IndexOf(obj);
    }

    public static ScriptableObject Get(int index) {
        return objectList[index];
    }
}
