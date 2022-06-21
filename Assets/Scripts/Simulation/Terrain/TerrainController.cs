using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TerrainController : MonoBehaviour, TickEventListener {
    public TickEvent tickEvent;
    public InventoryController userInventory;
    public ItemUI itemUI;
    [HideInInspector]
    public TerrainBlock[,] blocks;
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
    public bool rotateGrass = false;
    [Range(0f, 1f)]
    public float treeChance = 0.1f;
    public float minTreeSize = 75;
    public float maxTreeSize = 125;
    private void Awake() {
        blocks = new TerrainBlock[size, size];
        TerrainData data = SaveSystem.LoadTerrain();
        if (data != null)
            LoadTerrain(data);
        else
            Generate();
    }

    void Generate() {
        GameObject blockGameobject;
        GameObject chosenBlock;
        int halfSize = Mathf.FloorToInt(size * blockSpacing * .5f);
        for (int x = -halfSize, i = 0; x < halfSize; x += blockSpacing, i++) {
            for (int y = -halfSize, j = 0; y < halfSize; y += blockSpacing, j++) {
                chosenBlock = (Random.Range(0f, 1f) < fertileChance) ? fertileBlock : barrenBlock;
                blockGameobject = Instantiate(chosenBlock, transform.position + new Vector3(x, 0, y), Quaternion.identity, transform);
                blocks[i, j] = blockGameobject.GetComponent<TerrainBlock>();
                blocks[i, j].x = i;
                blocks[i, j].y = j;
                blocks[i, j].terrain = this;
                if (chosenBlock == fertileBlock) InitializeFertileBlock(i, j);
            }
        }
    }

    void InitializeFertileBlock(int i, int j) {
        FertileBlock block = (FertileBlock)blocks[i, j];
        if (Random.Range(0f, 1f) < grassChance) {
            Quaternion rotation = (rotateGrass) ? Quaternion.Euler(0, Random.Range(0f, 360f), 0) : Quaternion.identity;
            Grass seed = Instantiate(grassGameObject,
                block.transform.position + new Vector3(0, 0.5f, 0),
                rotation,
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

    public bool Plant(GameObject plantPrefab, FertileBlock block)
    {
        Plant plant = plantPrefab.GetComponent<Plant>();
        if (plant is Grass && null == block.grass) {
            Quaternion rotation = (rotateGrass) ? Quaternion.Euler(0, Random.Range(0f, 360f), 0) : Quaternion.identity;
            Grass seed = Instantiate(grassGameObject,
                block.transform.position + new Vector3(0, 0.5f, 0),
                rotation,
                block.transform).GetComponent<Grass>();
            Plant(seed, block);
            return true;
        }
        if (plant is TreeModel && null == block.tree) {
            TreeModel seed = Instantiate(plantPrefab,
                block.transform.position + new Vector3(0, 0.5f, 0),
                plantPrefab.transform.localRotation * Quaternion.Euler(0, Random.Range(0f, 360f), 0),
                block.transform).GetComponent<TreeModel>();
            seed.transform.localScale = Vector3.one * Random.Range(minTreeSize, maxTreeSize);
            Plant(seed, block);
            return true;
        }
        return false;
    }

    public void Plant(Plant seed, FertileBlock block) {
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

    public void Plant(int x, int y, Plant seed) {
        if (x > -1 && x < size && y > -1 && y < size && blocks[x, y] is FertileBlock) {
            FertileBlock block = (FertileBlock)blocks[x, y];
            seed.transform.parent = block.transform;
            seed.transform.position = block.transform.position + new Vector3(0, 0.5f, 0);
            Plant(seed, block);
            return;
        }
        Destroy(seed.gameObject);
    }

    public void ResetTerrain() {
        SaveSystem.ResetTerrain();
        ClearTerrain();
        Generate();
    }

    void LoadTerrain(TerrainData data) {
        size = data.size;
        GameObject blockGameobject;
        GameObject chosenBlock;
        int halfSize = Mathf.FloorToInt(size * blockSpacing * .5f);
        for (int x = -halfSize, i = 0; x < halfSize; x += blockSpacing, i++) {
            for (int y = -halfSize, j = 0; y < halfSize; y += blockSpacing, j++) {
                chosenBlock = (data.blocks[i, j].type is BlockType.fertile) ? fertileBlock : barrenBlock;
                blockGameobject = Instantiate(chosenBlock, 
                    transform.position + new Vector3(x, 0, y), 
                    Quaternion.identity, 
                    transform);
                blocks[i, j] = blockGameobject.GetComponent<TerrainBlock>();
                blocks[i, j].x = i;
                blocks[i, j].y = j;
                blocks[i, j].terrain = this;
                if (chosenBlock == fertileBlock) {
                    FertileBlock block = (FertileBlock)blocks[i, j];
                    block.wet = data.blocks[i, j].wet;
                    if (data.grass[i, j].exists) {
                        Grass seed = Instantiate(grassGameObject,
                            data.grass[i, j].position,
                            data.grass[i, j].rotation,
                            block.transform).GetComponent<Grass>();
                        seed.isTall = data.grass[i, j].isTall;
                        seed.age = data.grass[i, j].age;
                        if (seed.isTall) seed.Grow();
                        Plant(seed, block);
                    }
                    if (data.trees[i, j].exists) {
                        GameObject treeObject = FindTree(data.trees[i, j].treeGameobjectIndex);
                        TreeModel seed = Instantiate(treeObject,
                            data.trees[i, j].position,
                            data.trees[i, j].rotation,
                            block.transform).GetComponent<TreeModel>();
                        seed.transform.localScale = data.trees[i, j].scale;
                        seed.stage = (TreeStage)ScriptableObjectLocator.Get(data.trees[i, j].growthStageIndex);
                        seed.ChangeView();
                        Plant(seed, block);
                    }
                }
            }
        }
    }

    private void ClearTerrain() {
        foreach (TerrainBlock block in blocks) {
            if (block is FertileBlock) {
                FertileBlock fblock = (FertileBlock)block;
                if (fblock.grass != null) fblock.grass.CleanChop();
                if (fblock.tree != null) fblock.tree.Chop();
                Destroy(fblock);
            }
        }
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private GameObject FindTree(int index) {
        foreach (GameObject treeGameobject in treesGameObjects) {
            if (treeGameobject.GetComponent<TreeModel>().treeIndex == index)
                return treeGameobject;
        }
        return null;
    }

    public void FertilizeBlock(BarrenBlock block)
    {
        GameObject blockGameObject = block.gameObject;
        MeshRenderer meshRenderer = blockGameObject.GetComponent<MeshRenderer>();
        MeshRenderer meshRendererFertile = this.fertileBlock.GetComponent<MeshRenderer>();
        meshRenderer.material = meshRendererFertile.sharedMaterial;
        FertileBlock fertileBlockComponent = blockGameObject.AddComponent<FertileBlock>();
        fertileBlockComponent.tickEvent = tickEvent; 
        fertileBlockComponent.x = block.x;
        fertileBlockComponent.y = block.y;
        fertileBlockComponent.terrain = this;
        blocks[block.x, block.y] = fertileBlockComponent;
    }

    public void FoundMoney(int quantity)
    {
        userInventory.AddMoney(quantity);
    }

    public void OnTick() {
        SaveSystem.SaveTerrain(this);
    }

    private void OnEnable() {
        tickEvent.tickEvent += OnTick;
    }

    private void OnDisable() {
        tickEvent.tickEvent -= OnTick;
    }
}
