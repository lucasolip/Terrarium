using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertileBlock : MonoBehaviour, TerrainBlock
{
    public bool wet = false;

    public void Water() {
        wet = true;
    }
}
