using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    public LayerMask foodMask;
    PetController pet;
    Collider[] collisions = new Collider[1];

    void Start()
    {
        pet = GetComponent<PetController>();
    }

    private void FixedUpdate()
    {
        if (Physics.OverlapSphereNonAlloc(transform.position - transform.forward, 1f, collisions, foodMask) > 0)
        {
            pet.mood.Eat(pet, collisions[0].transform.GetComponent<Food>());
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position - transform.forward, 1f);
    }
}
