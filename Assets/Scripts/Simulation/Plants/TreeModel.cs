using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeModel : Plant
{
    public int treeIndex = 0;
    public GameObject foodGameobject;
    public TreeStage stage;
    public int timeBetweenFruitGrowth = 30;
    int growingTime = 0;
    GameObject treeView;

    private void Awake() {
        ChangeView();
    }

    public void ChangeView() {
        if (treeView != null) Destroy(treeView);
        treeView = Instantiate(stage.model, transform.position, transform.rotation * stage.model.transform.rotation, transform);
    }

    public override void OnTick()
    {
        //Debug.Log("Tree tick");
        growingTime++;
        if (stage.stageTime > 0 && growingTime > stage.stageTime) {
            stage = stage.nextStage;
            ChangeView();
            growingTime = 0;
        }
        if (stage.isFruitTree) {
            FruitTree fruitTree = treeView.GetComponent<FruitTree>();
            if (!fruitTree.fruitGrown && growingTime > fruitTree.fruitGrowTime) {
                fruitTree.GrowFruit(foodGameobject, stage.fruit);
            } else if (fruitTree.fruitGrown && growingTime > fruitTree.fruitGrowTime + fruitTree.fruitRipeTime) {
                fruitTree.RipeFruit();
                growingTime = 0;
            }
        }
    }

    public override void Chop() {
        terrainBlock.tree = null;
        Destroy(gameObject);
    }

}
