using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PetParametersUI : PetChangeEventListener
{
    private RectTransform hungerBar;
    private RectTransform energyBar;
    private RectTransform happinessBar;
    private RectTransform cleanlinessBar;

    private void Start()
    {
        hungerBar = transform.GetChild(0).Find("Foreground").GetComponent<RectTransform>();
        energyBar = transform.GetChild(1).Find("Foreground").GetComponent<RectTransform>();
        happinessBar = transform.GetChild(2).Find("Foreground").GetComponent<RectTransform>();
        cleanlinessBar = transform.GetChild(3).Find("Foreground").GetComponent<RectTransform>();
    }
    private void SetScale(RectTransform rectTransform, float value)
    {
        Vector3 scale = rectTransform.localScale;
        scale.x = value;
        rectTransform.localScale = scale;
    }

    public override void OnPetChange(int hunger, int energy, int happiness, int cleanliness)
    {
        SetScale(hungerBar, hunger/100f);
        SetScale(energyBar, energy / 100f);
        SetScale(happinessBar, happiness / 100f);
        SetScale(cleanlinessBar, cleanliness / 100f);
    }
}
