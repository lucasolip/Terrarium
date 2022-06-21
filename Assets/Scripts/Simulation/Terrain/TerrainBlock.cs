using UnityEngine;
public abstract class TerrainBlock : MonoBehaviour
{
    public TerrainController terrain;
    public int x, y;
    public abstract void Water();
}
