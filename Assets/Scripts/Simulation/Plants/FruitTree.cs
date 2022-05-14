using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitTree : MonoBehaviour
{
    public Transform[] fruitPositions;
    List<int> availableGaps;
    List<int> usedGaps;
    GameObject[] fruits;
    public int fruitGrowTime = 1;
    public int fruitRipeTime = 5;
    public bool fruitGrown = false;

    private void Awake()
    {
        availableGaps = new List<int>();
        usedGaps = new List<int>();
        for (int i = 0; i < fruitPositions.Length; i++) {
            availableGaps.Add(i);
        }
        fruits = new GameObject[fruitPositions.Length];
    }

    public void GrowFruit(GameObject fruitGameobject, ItemModel model)
    {
        int numFruit = Random.Range(0, availableGaps.Count);
        for (int i = 0; i < numFruit; i++) {
            int fruitGap = availableGaps[Random.Range(0, availableGaps.Count)];
            fruits[fruitGap] = Instantiate(fruitGameobject, fruitPositions[fruitGap].position, Quaternion.identity, transform);
            fruits[fruitGap].transform.localScale = new Vector3(
                fruitGameobject.transform.localScale.x / transform.localScale.x, 
                fruitGameobject.transform.localScale.y / transform.localScale.y, 
                fruitGameobject.transform.localScale.z / transform.localScale.z);
            fruits[fruitGap].GetComponent<Rigidbody>().isKinematic = true;
            fruits[fruitGap].GetComponent<ItemController>().model = model;
            fruits[fruitGap].GetComponent<MouseFollower>().enabled = false;
            availableGaps.Remove(fruitGap);
            usedGaps.Add(fruitGap);
        }
        fruitGrown = true;
    }

    public void RipeFruit()
    {
        foreach (int i in usedGaps) {
            if (null != fruits[i]) {
                fruits[i].GetComponent<Rigidbody>().isKinematic = false;
                fruits[i].GetComponent<MouseFollower>().enabled = true;
                Vector3 position = fruits[i].transform.position;
                fruits[i].transform.SetParent(null);
                fruits[i].transform.position = position;
                fruits[i] = null;
                availableGaps.Add(i);
            }
        }
        usedGaps.Clear();
        fruitGrown = false;
    }
}