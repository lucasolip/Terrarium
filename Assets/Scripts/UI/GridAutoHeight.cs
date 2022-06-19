using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class GridAutoHeight : MonoBehaviour
{
    GridLayoutGroup grid;
    RectTransform rectTransform;

    private void Awake() {
        grid = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
        float nCols = GetColumnCount();
        float gridHeight = grid.cellSize.y * nCols + grid.spacing.y * nCols + grid.padding.top + grid.padding.bottom;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, gridHeight);
    }

    private float GetColumnCount()
    {
        int numElements = transform.childCount;
        int elementsPerRow = Mathf.FloorToInt((rectTransform.sizeDelta.x + grid.padding.left + grid.padding.right) / (grid.cellSize.x + grid.spacing.x));
        return 1 + numElements / elementsPerRow;
    }
}
