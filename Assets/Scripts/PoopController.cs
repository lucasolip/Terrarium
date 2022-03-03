using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopController : MonoBehaviour
{
    private void OnMouseDown() {
        Destroy(gameObject);
    }
}
