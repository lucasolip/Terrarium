using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeModel : Plant
{
    public TreeStage _stage;
    int growingTime = 0;
    GameObject treeView;

    TreeStage stage {
        set {
            _stage = value;
        }
    }

    private void Awake() {
        treeView = Instantiate(_stage.model, transform.position, transform.rotation * _stage.model.transform.rotation, transform);
    }

    public override void OnTick()
    {
        Debug.Log("Tree tick");
        growingTime++;
        if (_stage.stageTime > 0 && growingTime > _stage.stageTime) {
            _stage = _stage.nextStage;
            Destroy(treeView);
            treeView = Instantiate(_stage.model, transform.position, transform.rotation * _stage.model.transform.rotation, transform);
            growingTime = 0;
        }
    }

}
