using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelAction : MouseHandler
{
    FertileBlock block;
    InventoryController userInventory;
    bool clicked;

    private void Awake()
    {
        block = GetComponent<FertileBlock>();
        userInventory = GameObject.Find("User").GetComponent<InventoryController>();
    }

    public override void Clicked()
    {
        clicked = true;
    }

    public override void Dragged()
    {
    }

    public override void Released()
    {
        if (MouseController.selectedTool.tool == Tool.Shovel && clicked)
        {
            ItemModel item = block.terrain.itemUI.GetCurrentItem();
            if (item.isPlantable)
            {
                bool planted = block.Plant(item.plantPrefab);
                if (planted) userInventory.RemoveItem(item);
                else Debug.Log("Not planted");
            }
        }
        clicked = false;
    }



}
