using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopController : MouseHandler
{
    public override void Released()
    {
    }

    public override void Dragged()
    {
    }

    public override void Clicked()
    {
        Destroy(gameObject);
    }
}
