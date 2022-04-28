using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    [Header("Movement")]
    public float maxForce = 0.1f;
    public float maxSpeed = 1f;
    public float wanderRadius = 1f;
    public float wanderDistance = 1f;
    public float wanderAngle = 30f;
    public float angleChange = 1f;
    public float wanderWeight = 1f;
    public float avoidWeight = 10f;
    [Header("Perception")]
    public LayerMask foodMask;
    public LayerMask floorMask;
    PetController pet;
    Rigidbody rb;
    Collider[] collisions = new Collider[1];
    bool asleep = false;

    void Start()
    {
        pet = GetComponent<PetController>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (asleep && !(pet.mood is AsleepMood))
        {
            asleep = false;
        } else if (!asleep && pet.mood is AsleepMood) {
            asleep = true;
            rb.velocity = Vector3.zero;
        }
        if (!asleep) Behaviour();
        if (Physics.OverlapSphereNonAlloc(transform.position - transform.forward, 1f, collisions, foodMask) > 0)
        {
            pet.mood.Eat(pet, collisions[0].transform.GetComponent<FoodController>());
        }
    }

    private void Behaviour()
    {
        Vector3 steer = wanderWeight * Wander();
        if (!Physics.Raycast(transform.position - transform.forward, Vector3.down, 2f, floorMask)) 
            steer = avoidWeight * Avoid();
        steer = Vector3.ClampMagnitude(steer, maxForce);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity + steer, maxSpeed);
    }

    private Vector3 Wander()
    {
        Vector3 circleCenter = rb.velocity;
        circleCenter.Normalize();
        circleCenter *= wanderDistance;
        Vector3 displacement = new Vector3(0, 0, -1);
        displacement *= wanderRadius;
        displacement = Quaternion.Euler(0, wanderAngle, 0) * displacement;
        wanderAngle += (Random.Range(0f, 1f) * angleChange) - (angleChange * .5f);

        return circleCenter + displacement;
    }

    private Vector3 Avoid()
    {
        return -rb.velocity;
    }

    private void Seek()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position - transform.forward, 1f);
        Gizmos.DrawRay(transform.position-transform.forward, Vector3.down);
    }
}
