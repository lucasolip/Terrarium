using UnityEngine;
using DateTime = System.DateTime;

public class PetController : MonoBehaviour, TickEventListener
{
    public TickEvent tickEvent;
    public PetChangeEvent petChangeEvent;
    public PetDiedEvent petDiedEvent;
    public string petName;
    public PetMood mood;
    public PetStage stage;
    public DateTime bornTime;
    [Header("Parameters")]
    public int hunger = 100;
    public int happiness = 100;
    public int cleanliness = 100;
    public int energy = 100;
    [Header("Poop")]
    public int poops = 0;
    public float poopDistance = 1f;
    public GameObject poopPrefab;

    private int age;
    private Material material;
    private bool dead = false;

    public void Start()
    {
        mood = new FineMood();
        material = GetComponent<Renderer>().material;
        mood.OnEnter(stage, null, material);
        tickEvent.tickEvent += OnTick;
    }

    public void OnTick()
    {
        //Debug.Log("Pet tick");
        PetMood nextMood = mood.UpdateParameters(this, stage);
        if (null != nextMood) {
            PetMood previousMood = mood;
            mood = nextMood;
            mood.OnEnter(stage, previousMood, material);
        }
        petChangeEvent.Raise(hunger, energy, happiness, cleanliness);
        PetStage nextStage = stage.Age(ref age);
        if (null != nextStage){
            stage = nextStage;
            material.mainTexture = mood.GetModel(stage);
        }
        ClampParameters();
        if (poops > 0 && Random.Range(0f,1f) > .8f) Poop();
        if (!dead) SaveSystem.SavePet(this, false);
    }

    public void CheckMood() {
        PetMood nextMood = mood.CheckMood(this, stage);
        if (null != nextMood) {
            PetMood previousMood = mood;
            mood = nextMood;
            mood.OnEnter(stage, previousMood, material);
        }
        petChangeEvent.Raise(hunger, energy, happiness, cleanliness);
    }

    public void ClampParameters()
    {
        hunger = Mathf.Clamp(hunger, 0, 100);
        happiness = Mathf.Clamp(happiness, 0, 100);
        energy = Mathf.Clamp(energy, 0, 100);
        cleanliness = Mathf.Clamp(cleanliness, 0, 100);
    }

    public void Die()
    {
        dead = true;
        SaveSystem.SavePet(this, true);
        Debug.Log("Pet died :(");
        petDiedEvent.Raise(this);
    }

    private void Poop() {
        Transform itemParent = GameObject.Find("Items").transform;
        Instantiate(poopPrefab, 
            transform.position + (new Vector3(Random.Range(-2f,2f),0,Random.Range(-2f,2f))) * poopDistance, 
            poopPrefab.transform.rotation, itemParent);
        poops -= 1;
    }

    public void SimulateGrowth()
    {
        age = Mathf.FloorToInt((float)(DateTime.Now - bornTime).TotalSeconds / Clock._tickPeriod);
        while (stage.nextStage != null && age > stage.stageTime) {
            stage = stage.nextStage;
        }
    }

    private void OnDestroy()
    {
        tickEvent.tickEvent -= OnTick;
    }
}
