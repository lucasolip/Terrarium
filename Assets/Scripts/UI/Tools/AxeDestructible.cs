using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeDestructible : MouseHandler
{
    bool clicked = false;

    public override void Clicked()
    {
        clicked = true;
    }

    public override void Dragged()
    {
    }

    public override void Released()
    {
        if (MouseController.selectedTool.tool == Tool.Axe && clicked) {
            Plant plant = GetComponent<Plant>();
            if (plant == null && transform.parent != null) 
                plant = transform.parent.GetComponent<Plant>();
            if (plant != null) {
                plant.Chop();
            } else {
                Debug.LogWarning(gameObject.name + " is not a Plant");
                Destroy(gameObject);
            }
        }
        clicked = false;
    }
}
