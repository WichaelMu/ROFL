using System.Collections;
using UnityEngine;

public class BrandW : MonoBehaviour
{
    public float ScaleDelta;
    public float MaximumScale;

    IEnumerator Ability;
    float damage;
    
    void Start()
    {
        Ability = W();
        StartCoroutine(Ability);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            other.GetComponent<Character>().TakeDamage(damage);
            Debug.Log("hit");
        }

        Debug.Log(name);
    }

    IEnumerator W()
    {
        Vector3 scale = new Vector3(ScaleDelta, ScaleDelta, ScaleDelta);
        yield return new WaitForSeconds(.5f);
        while (true)
        {
            transform.localScale += scale;
            if (transform.localScale.x >= MaximumScale)
                break;
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(.5f);
        while (true)
        {
            transform.localScale -= scale;
            if (transform.localScale.x <= 0)
                break;
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }

    public void Set(float damage) => this.damage = damage;
}
