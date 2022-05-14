using System;

enum BlockType {
    barren, fertile
}

struct TreeData {
    SerializableVector3 position;
    SerializableQuaternion rotation;
    SerializableVector3 scale;
    int blockX;
    int blockY;
    string growthStage;
}

struct GrassData {
    SerializableVector3 position;
    SerializableQuaternion rotation;
    SerializableVector3 scale;
    int blockX;
    int blockY;
    bool isTall;
}

[System.Serializable]
public class TerrainData {
    int size;
    BlockType[,] blocks;
    TreeData[,] trees;
    GrassData[,] grass;

    public TerrainData(TerrainController terrain) {
        size = terrain.size;
        blocks = new BlockType[size, size];
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                TerrainBlock block = terrain.blocks[i, j];
                blocks[i, j] = (block is FertileBlock) ? BlockType.fertile : BlockType.barren;
            }
        }
    }
}
