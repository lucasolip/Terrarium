using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UIAnimator))]
public class PetParametersUI : MonoBehaviour, PetChangeEventListener, PetBornEventListener
{
    public PetChangeEvent petChangeEvent;
    public PetBornEvent petBornEvent;
    public PetDiedEvent petDiedEvent;
    private RectTransform hungerBar;
    private RectTransform energyBar;
    private RectTransform happinessBar;
    private RectTransform cleanlinessBar;
    private RectTransform bars;
    private bool shown = false;
    private ShowUIAudioManager showAudioManager;
    private UIAnimator animator;

    private void Awake() {
        petBornEvent.petBornEvent += OnPetBorn;
        petDiedEvent.petDiedEvent += OnPetDied;
        bars = transform.GetChild(0).GetComponent<RectTransform>();
        bars.gameObject.SetActive(shown);
        hungerBar = bars.GetChild(0).Find("Foreground").GetComponent<RectTransform>();
        energyBar = bars.GetChild(1).Find("Foreground").GetComponent<RectTransform>();
        happinessBar = bars.GetChild(2).Find("Foreground").GetComponent<RectTransform>();
        cleanlinessBar = bars.GetChild(3).Find("Foreground").GetComponent<RectTransform>();
        bars.localScale = new Vector3(1, 0, 1);
        showAudioManager = GetComponent<ShowUIAudioManager>();
        animator = GetComponent<UIAnimator>();
        gameObject.SetActive(false);
    }

    private void SetScale(RectTransform rectTransform, float value)
    {
        Vector3 scale = rectTransform.localScale;
        scale.x = value;
        rectTransform.localScale = scale;
    }

    public void Show()
    {
        StopAllCoroutines();
        if (!shown) ShowUI();
        else HideUI();
    }

    private void HideUI() {
        showAudioManager.HideSound();
        shown = false;
        animator.ScaleOut(bars.gameObject, Hide);
    }

    private void Hide() {
        bars.gameObject.SetActive(false);
    }

    private void ShowUI() {
        showAudioManager.ShowSound();
        shown = true;
        bars.gameObject.SetActive(true);
        animator.ScaleIn(bars.gameObject);
    }

    private void OnEnable()
    {
        petChangeEvent.petChangeEvent += OnPetChange;
    }

    private void OnDisable()
    {
        petChangeEvent.petChangeEvent -= OnPetChange;
    }

    private void OnDestroy() {
        petBornEvent.petBornEvent -= OnPetBorn;
        petDiedEvent.petDiedEvent -= OnPetDied;
    }

    public void OnPetBorn(PetController pet) {
        gameObject.SetActive(true);
    }

    public void OnPetDied(PetController pet) {
        gameObject.SetActive(false);
    }

    public void OnPetChange(int hunger, int energy, int happiness, int cleanliness)
    {
        SetScale(hungerBar, hunger/100f);
        SetScale(energyBar, energy / 100f);
        SetScale(happinessBar, happiness / 100f);
        SetScale(cleanlinessBar, cleanliness / 100f);
    }
}
