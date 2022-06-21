using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    public ParticleSystem[] particleSystems;

    public void Play(int index)
    {
        Instantiate(particleSystems[index].gameObject, transform.position, Quaternion.identity, transform.parent);
    }
}
