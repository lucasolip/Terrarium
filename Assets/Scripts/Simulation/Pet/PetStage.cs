using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/PetStage")]
public class PetStage : ScriptableObject
{
    public string stageName;
    public PetStage nextStage;
    public Texture fineModel;
    public Texture sadModel;
    public Texture sickModel;
    public Texture asleepModel;
    public int poopsPerMeal;
    public int hungerPerTick;
    public int happinessPerTick;
    public int energyPerTick;
    public int energyRecoveryAsleep;
    public int stageTime;
    public int maxPoops;
    public int sickTime;
    public int timeToGetSick;

    public PetStage Age(ref int age)
    {
        age++;
        if (age > stageTime && stageTime > 0)
        {
            return nextStage;
        }
        return null;
    }
}