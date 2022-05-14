using UnityEngine;
using DateTime = System.DateTime;

public class PetController : TickEventListener
{
    public PetChangeEvent petChangeEvent;
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

    private Material material;

    public void Awake()
    {
        bornTime = DateTime.Now;
        mood = new FineMood();
        material = GetComponent<Renderer>().sharedMaterial;
        mood.OnEnter(stage, null, material);
    }

    public override void OnTick()
    {
        Debug.Log("Pet tick");
        PetMood nextMood = mood.UpdateParameters(this, stage);
        if (null != nextMood) {
            PetMood previousMood = mood;
            mood = nextMood;
            mood.OnEnter(stage, previousMood, material);
        }
        petChangeEvent.Raise(hunger, energy, happiness, cleanliness);
        PetStage nextStage = stage.Update();
        if (null != nextStage) stage = nextStage;
        ClampParameters();
        if (poops > 0 && Random.Range(0f,1f) > .8f) Poop();
        SaveSystem.SavePet(this);
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
        // Instantiate pet angel prefab
        Debug.Log("Pet died :(");
        Destroy(gameObject);
    }

    private void Poop() {
        Instantiate(poopPrefab, transform.position + (new Vector3(Random.Range(-2f,2f),0,Random.Range(-2f,2f))) * poopDistance, poopPrefab.transform.rotation);
        poops -= 1;
    }
    private void OnApplicationQuit()
    {
        stage.currentAge = 0;
    }
}
