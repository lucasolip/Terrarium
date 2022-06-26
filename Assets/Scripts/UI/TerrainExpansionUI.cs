using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TerrainExpansionUI : MonoBehaviour
{
    public TerrainController terrain;
    public InventoryController userInventory;
    public string textToShow = "¿Expandir terreno por ";
    private TextMeshProUGUI shopText;

    private void Awake()
    {
        shopText = transform.Find("TextPanel").Find("Text").GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        shopText.text = textToShow + GetExpansionPrice() + "<sprite name=\"coinIcon\">?";
    }

    public void BuyTerrainExpansion()
    {
        bool valid = userInventory.SubstractMoney(GetExpansionPrice());
        if (valid) {
            terrain.Expand();
            shopText.text = textToShow + GetExpansionPrice() + "<sprite name=\"coinIcon\">?";
        }
    }

    private int GetExpansionPrice()
    {
        return 4 * terrain.size + 4;
    }

}
