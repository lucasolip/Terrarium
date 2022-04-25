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
    public GameObject[] treesGameObjects;
    public int size = 16;
    public int blockSpacing = 2;
    [Range(0f, 1f)]
    public float fertileChance = 0.8f;
    [Header("Plant settings")]
    [Range(0f, 1f)]
    public float grassChance = 0.2f;
    [Range(0f, 1f)]
    public float treeChance = 0.1f;
    public float minTreeSize = 75;
    public float maxTreeSize = 125;
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
        FertileBlock block = (FertileBlock)blocks[i, j];
        block.x = i;
        block.y = j;
        block.terrain = this;
        if (Random.Range(0f, 1f) < grassChance) {
            Grass seed = Instantiate(grassGameObject, 
                block.transform.position + new Vector3(0, 0.5f, 0),
                Quaternion.Euler(0, Random.Range(0f, 360f), 0), 
                block.transform).GetComponent<Grass>();
            Plant(seed, block);
        }
        if (treesGameObjects.Length > 0 && Random.Range(0f, 1f) < treeChance) {
            GameObject treeObject = treesGameObjects[Mathf.FloorToInt(Random.Range(0f, treesGameObjects.Length))];
            TreeModel seed = Instantiate(treeObject,
                block.transform.position + new Vector3(0, 0.5f, 0),
                treeObject.transform.localRotation * Quaternion.Euler(0, Random.Range(0f, 360f), 0),
                block.transform).GetComponent<TreeModel>();
            seed.transform.localScale = Vector3.one * Random.Range(minTreeSize, maxTreeSize);
            Plant(seed, block);
        }
    }

    public void Plant(Plant seed, FertileBlock block)
    {
        if (seed is Grass && null == block.grass) {
            Grass grassSeed = (Grass)seed;
            grassSeed.terrainBlock = block;
            block.grass = grassSeed;
            return;
        }
        if (seed is TreeModel && null == block.tree) {
            TreeModel treeSeed = (TreeModel)seed;
            treeSeed.terrainBlock = block;
            block.tree = treeSeed;
            return;
        }
        Destroy(seed.gameObject);
    }

    public void Plant(int x, int y, Plant seed)
    {
        if (x > -1 && x < size && y > -1 && y < size && blocks[x, y] is FertileBlock) {
            FertileBlock block = (FertileBlock)blocks[x, y];
            seed.transform.parent = block.transform;
            seed.transform.position = block.transform.position + new Vector3(0, 0.5f, 0);
            Plant(seed, block);
            return;
        }
        Destroy(seed.gameObject);
    }
}
