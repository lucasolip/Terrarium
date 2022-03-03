using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    TerrainBlock[,] blocks;
    [Header("Generation settings")]
    public GameObject fertileBlock;
    public GameObject barrenBlock;
    public int size = 16;
    public int blockSpacing = 2;
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
                chosenBlock = (Random.Range(0f, 1f) < .8f)? fertileBlock : barrenBlock;
                blockGameobject = Instantiate(chosenBlock, transform.position + new Vector3(x, 0, y), Quaternion.identity, transform);
                blocks[i, j] = blockGameobject.GetComponent<TerrainBlock>();
            }
        }
    }
}
