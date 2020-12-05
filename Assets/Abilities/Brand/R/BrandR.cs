using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BrandR : MonoBehaviour
{
    Rigidbody rb;
    Transform target;

    public float TurnRadius;
    public float velocity;
    float damage;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Travel();
    }

    void Travel()
    {
        GLOBAL.Homing(rb, target, velocity, TurnRadius);

        if (GLOBAL.HasReached(transform.position, target.position))
            DealDamage();
    }

    void DealDamage()
    {
        target.GetComponent<Character>().TakeDamage(damage);

        Destroy(gameObject);
    }

    public void Set(Transform target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }
}
