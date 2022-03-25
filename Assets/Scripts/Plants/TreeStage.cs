using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plants/TreeStage")]
public class TreeStage : ScriptableObject
{
    public GameObject model;
    public TreeStage nextStage;
    public int ticksWithoutWater;
    public int stageTime;
}
