using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeDestructible : MouseHandler
{
    public override void Clicked()
    {
    }

    public override void Dragged()
    {
    }

    public override void Released()
    {
        if (MouseController.selectedTool.tool == Tool.Pickaxe)
        {
            
        }
    }
}