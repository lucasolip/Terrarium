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
    public float fixedHeight = 1f;
    [Header("Perception")]
    public LayerMask foodMask;
    public LayerMask floorMask;
    PetController pet;
    Rigidbody rb;
    Collider[] collisions = new Collider[1];
    bool asleep = false;
    Vector3 forward = Vector3.forward;
    Vector3 planeVelocity;

    void Start()
    {
        pet = GetComponent<PetController>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        planeVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (planeVelocity.magnitude > 0.1f) forward = planeVelocity.normalized;
        if (asleep && !(pet.mood is AsleepMood))
        {
            asleep = false;
        } else if (!asleep && pet.mood is AsleepMood) {
            asleep = true;
            rb.velocity = Vector3.zero;
        }
        if (!asleep) Behaviour();
        rb.position = new Vector3(rb.position.x, fixedHeight, rb.position.z);

        if (Physics.OverlapSphereNonAlloc(transform.position - transform.forward, 1f, collisions, foodMask) > 0)
        {
            pet.mood.Eat(pet, collisions[0].transform.GetComponent<FoodController>());
        }

    }

    private void Behaviour()
    {
        Vector3 steer = wanderWeight * Wander() + avoidWeight * Avoid();
        steer = Vector3.ClampMagnitude(steer, maxForce);
        Vector3 velocity = Vector3.ClampMagnitude(rb.velocity + steer, maxSpeed);
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        //rb.AddForce(steer, ForceMode.VelocityChange);
    }

    private Vector3 Wander()
    {
        Vector3 circleCenter = forward;
        circleCenter.Normalize();
        circleCenter *= wanderDistance;
        Vector3 displacement = circleCenter.normalized;
        displacement *= wanderRadius;
        displacement = Quaternion.Euler(0, wanderAngle, 0) * displacement;
        wanderAngle += (Random.Range(0f, 1f) * angleChange) - (angleChange * .5f);

        return circleCenter + displacement;
    }

    private Vector3 Avoid()
    {
        if (!Physics.Raycast(transform.position + forward, Vector3.down, 2f, floorMask))
            return Quaternion.Euler(0, 90, 0) * forward;
        return Vector3.zero;
    }

    private void Seek()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + forward, 1f);
        Gizmos.DrawRay(transform.position + forward, Vector3.down);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 90, 0) * forward);
    }
}
