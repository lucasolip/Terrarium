using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWorldCheckController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " hit End World");
        Destroy(other.gameObject);
    }
}
