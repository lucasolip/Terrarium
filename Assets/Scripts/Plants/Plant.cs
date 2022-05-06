using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Plant : TickEventListener
{
    public FertileBlock terrainBlock;

    public abstract void Chop();
}
