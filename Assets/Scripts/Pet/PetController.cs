using UnityEngine;

public class PetController : MonoBehaviour, TickEventListener
{
    public string petName;
    public TickEvent tickEvent;
    public PetMood mood;
    public PetStage stage;
    public int hunger = 100;
    public int happiness = 100;
    public int cleanliness = 100;
    public int energy = 100;

    private Material material;

    public void Awake()
    {
        mood = new FineMood();
        material = GetComponent<Renderer>().sharedMaterial;
        mood.OnEnter(stage, null, material);
    }

    public void OnTick()
    {
        Debug.Log("Pet tick");
        PetMood nextMood = mood.UpdateParameters(this, stage);
        if (null != nextMood)
        {
            PetMood previousMood = mood;
            mood = nextMood;
            mood.OnEnter(stage, previousMood, material);
        }
        PetStage nextStage = stage.Update();
        if (null != nextStage)
        {
            stage = nextStage;
        }
        ClampParameters();
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
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        tickEvent.tickEvent += OnTick;
    }

    private void OnDisable()
    {
        tickEvent.tickEvent -= OnTick;
    }
    private void OnApplicationQuit()
    {
        stage.currentAge = 0;
    }
}
