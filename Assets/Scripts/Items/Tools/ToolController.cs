using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tool {
    Pickaxe, Axe, Shovel, WateringCan, Hand
}

public class ToolController : MonoBehaviour
{
    public LayerMask layerMask;
    public Tool tool;
}