using System;
using UnityEngine;

public class AutoProjectile : MonoBehaviour
{

    Transform target;
    Vector3 StartingPosition;
    float damage;

    void Start()
    {
        StartingPosition = transform.position;
    }

    float t = 0;

    void FixedUpdate()
    {

        Travel();
    }

    void Travel()
    {
        t += Time.fixedDeltaTime;
        transform.position = Vector3.Lerp(StartingPosition, target.position, Easing.Linear(t, 0, 1, 1.5f));

        if (Vector3.Distance(transform.position, target.position) < .1f)
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
