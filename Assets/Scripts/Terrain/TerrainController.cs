using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    TerrainBlock[,] blocks;
    [Header("Generation settings")]
    public GameObject fertileBlock;
    public GameObject barrenBlock;
    public GameObject grassGameObject;
    public int size = 16;
    public int blockSpacing = 2;
    [Range(0f, 1f)]
    public float fertileChance = 0.8f;
    [Range(0f, 1f)]
    public float grassChance = 0.2f;
    private void Awake() {
        blocks = new TerrainBlock[size, size];
        Generate();
    }

    void Generate() {
        GameObject blockGameobject;
        GameObject chosenBlock;
        int halfSize = Mathf.FloorToInt(size*blockSpacing*.5f);
        for (int x = -halfSize, i = 0; x < halfSize; x += blockSpacing, i++) {
            for (int y = -halfSize, j = 0; y < halfSize; y += blockSpacing, j++) {
                chosenBlock = (Random.Range(0f, 1f) < fertileChance) ? fertileBlock : barrenBlock;
                blockGameobject = Instantiate(chosenBlock, transform.position + new Vector3(x, 0, y), Quaternion.identity, transform);
                blocks[i, j] = blockGameobject.GetComponent<TerrainBlock>();
                if (chosenBlock == fertileBlock) InitializeFertileBlock(i, j);
            }
        }
    }

    void InitializeFertileBlock(int i, int j)
    {
        FertileBlock fblock = (FertileBlock)blocks[i, j];
        fblock.x = i;
        fblock.y = j;
        fblock.terrain = this;
        if (Random.Range(0f, 1f) < grassChance) {
            Grass seed = Instantiate(grassGameObject).GetComponent<Grass>();
            Plant(i, j, seed);
        }
    }

    public void Plant(int x, int y, Plant seed)
    {
        Grass grassSeed = (Grass)seed;
        if (x > -1 && x < size && y > -1 && y < size && blocks[x, y] is FertileBlock) {
            FertileBlock block = (FertileBlock) blocks[x, y];
            if (seed is Grass && null == block.grass) {
                grassSeed.transform.position = block.transform.position + new Vector3(0, 0.5f, 0);
                grassSeed.transform.localScale.Scale(block.transform.localScale);
                grassSeed.transform.parent = block.transform;
                grassSeed.terrainBlock = block;
                block.grass = grassSeed;
                return;
            }
        }
        Destroy(grassSeed.gameObject);
    }
}
