using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AutoProjectile : MonoBehaviour
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

        if (GLOBAL.HasReached(transform.position, target.position, transform.localScale.x * .5f))
            DealDamage();
    }

    void DealDamage()
    {
        target.GetComponent<Character>().TakeDamage(damage);

        Destroy(gameObject);
    }

    /// <summary>
    /// Sets the parameters for this projectile.
    /// </summary>
    /// <param name="t">Transform target to track.</param>
    /// <param name="Damage">Float damage to deal.</param>
    public void Set(Transform t, float Damage)
    {
        target = t;
        damage = Damage;
    }
}
