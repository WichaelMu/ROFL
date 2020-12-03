using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject Auto;

    [HideInInspector]
    public Transform target;
    ClickMove PlayerNav;

    public string _name;
    /// <summary>The maximum health.</summary>
    public float MaxHealth = 2015;
    float CurrentHealth;
    public float AutoDamage = 10f;
    /// <summary>The auto range for this character.</summary>
    public float AutoRange = 10;
    public float AttackSpeed;
    /// <summary>This amount of money this character has.</summary>
    int money = 1000;
    /// <summary>This character's list of items.</summary>
    List<Item> items;

    bool chasing = false;

    void Start()
    {
        PlayerNav = GetComponent<ClickMove>();
        Spawn();
    }

    void Update()
    {
        BasicAttack();
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
            GetComponent<Renderer>().material.color = Color.red;

        //Debug.Log(_name + " " + CurrentHealth);
    }

    void Spawn()
    {
        CurrentHealth = MaxHealth;
    }

    void Buy(Item i)
    {
        items.Add(i);
    }

    void Sell(Item i)
    {
        items.Remove(i);
    }

    void BasicAttack()
    {
        #region ignore
        //Ray ray = new Ray(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //if (Physics.Raycast(ray, out RaycastHit hit, range, 9))
        //{
        //    Transform t = hit.collider.gameObject.transform;
        //    Character c = hit.collider.GetComponent<Character>();
        //    if (c != null && c.range >= Vector3.Distance(transform.position, t.position))
        //    {

        //    }
        //}

        #endregion

        if (GLOBAL.RightClick())
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                target = hit.collider.gameObject.transform;
                Character c = hit.collider.GetComponent<Character>();

                if (c != null && c != this)
                {
                    //  In range.
                    if (GLOBAL.HasReached(transform.position, target.position, AutoRange))
                        AutoAttack(target);

                    chasing = true;
                }
                else
                {
                    target = null;
                    c = null;
                    chasing = false;
                }
            }

        //  Chase target.
        if (chasing)
            if (GLOBAL.HasReached(transform.position, target.position, AutoRange))
                AutoAttack(target);
    }

    void AutoAttack(Transform t)
    {
        GameObject auto = Instantiate(Auto, transform.position, Quaternion.identity);
        auto.GetComponent<AutoProjectile>().Set(t, AutoDamage);

        //  Stop moving when in range.
        PlayerNav.StopMoving();

        //  Move towards the target after .5 seconds.
        Invoke("MaintainDistance", .5f);
    }

    /// <summary>Move towards the target at maximum range.</summary>
    void MaintainDistance()
    {
        if (target != null)
            PlayerNav.MoveTo(target.position);
    }
}