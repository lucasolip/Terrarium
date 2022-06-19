using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MouseHandler : MonoBehaviour
{
    public abstract void Clicked();
    public abstract void Released();
    public abstract void Dragged();
}
