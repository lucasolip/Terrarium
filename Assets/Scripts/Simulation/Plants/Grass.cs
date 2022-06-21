using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Plant
{
    [Header("Plant settings")]
    public GameObject grassGameobject;
    public Vector3 shortScale;
    public Vector3 tallScale;
    public int reproduceAge;
    public int growAge;
    [Header("Item settings")]
    [Range(0, 1)]
    public float moneyChance = 0.1f;
    public int moneyQuantity = 1;

    private ParticlePlayer particlePlayer;
    private AudioPlayer audioPlayer;

    public bool isTall;
    public int age = 0;
    Collider objectCollider;

    private void Awake()
    {
        particlePlayer = GetComponent<ParticlePlayer>();
        audioPlayer = GetComponent<AudioPlayer>();
        objectCollider = GetComponent<Collider>();
        transform.localScale = shortScale;
        isTall = false;
    }

    public override void OnTick()
    {
        Debug.Log("Grass tick");
        age++;
        if (!isTall && age > growAge) {
            Grow();
        }
        if (age > reproduceAge) {
            Reproduce();
        }
    }

    public void Grow() {
        isTall = true;
        objectCollider.enabled = true;
        transform.localScale = tallScale;
    }

    void Reproduce()
    {
        terrainBlock.PlantNeighbour(grassGameobject);
    }

    public override void Chop()
    {
        terrainBlock.grass = null;
        audioPlayer.Play(0);
        particlePlayer.Play(0);
        if (Random.Range(0f, 1f) < moneyChance) {
            particlePlayer.Play(1);
            terrainBlock.terrain.FoundMoney(moneyQuantity);
        }
        Destroy(gameObject);
    }

    public void CleanChop()
    {
        terrainBlock.grass = null;
        Destroy(gameObject);
    }
}
