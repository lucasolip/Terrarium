using System;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public enum BlockType {
    barren, fertile
}

[System.Serializable]
public struct BlockData {
    public BlockType type;
    public bool wet;
}

[System.Serializable]
public struct TreeData {
    public bool exists;
    public SerializableVector3 position;
    public SerializableQuaternion rotation;
    public SerializableVector3 scale;
    public int growthStageIndex;
    public int treeGameobjectIndex;
}

[System.Serializable]
public struct GrassData {
    public bool exists;
    public SerializableVector3 position;
    public SerializableQuaternion rotation;
    public SerializableVector3 scale;
    public bool isTall;
    public int age;
}

[System.Serializable]
public class TerrainData {
    public int size;
    public BlockData[,] blocks;
    public TreeData[,] trees;
    public GrassData[,] grass;

    public TerrainData(TerrainController terrain) {
        size = terrain.size;
        blocks = new BlockData[size, size];
        trees = new TreeData[size, size];
        grass = new GrassData[size, size];
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                TerrainBlock block = terrain.blocks[i, j];
                if (block is FertileBlock) {
                    blocks[i, j].type = BlockType.fertile;

                    TreeModel blockTree = ((FertileBlock)block).tree;
                    if (blockTree != null) {
                        trees[i, j] = InitializeTree(trees[i, j], blockTree);
                    } else 
                        trees[i, j].exists = false;

                    Grass blockGrass = ((FertileBlock)block).grass;
                    if (blockGrass != null) {  
                        grass[i, j] = InitializeGrass(grass[i, j], blockGrass);
                        Debug.Log(grass[i, j].exists);
                    } else 
                        grass[i, j].exists = false;
                } else {
                    blocks[i, j].type = BlockType.barren;
                }
            }
        }
    }

    private TreeData InitializeTree(TreeData data, TreeModel blockTree) {
        data.exists = true;
        data.position = blockTree.transform.position;
        data.rotation = blockTree.transform.rotation;
        data.scale = blockTree.transform.localScale;
        data.growthStageIndex = ScriptableObjectLocator.GetIndex(blockTree.stage);
        data.treeGameobjectIndex = blockTree.treeIndex;
        return data;
    }

    private GrassData InitializeGrass(GrassData data, Grass blockGrass) {
        data.exists = true;
        data.position = blockGrass.transform.position;
        data.rotation = blockGrass.transform.rotation;
        data.scale = blockGrass.transform.localScale;
        data.isTall = blockGrass.isTall;
        data.age = blockGrass.age;
        return data;
    }
}
