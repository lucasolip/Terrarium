using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureLocator : MonoBehaviour
{
    public Texture _nullTexture;
    public static Texture nullTexture;

    private void Awake()
    {
        nullTexture = _nullTexture;
    }
}
