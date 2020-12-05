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
        items = new List<Item>();
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

    public ClickMove GetPlayerNav()
    {
        return PlayerNav;
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
                Character c = hit.collider.GetComponent<Character>();

                //  If a character is hit and is not yourself.
                if (c != null && c != this)
                {
                    //  Set the target to the clicked character.
                    target = c.transform;

                    //  In range.
                    if (GLOBAL.HasReached(transform.position, target.position, AutoRange))
                        AutoAttack();

                    //  Chase this character.
                    chasing = true;
                }
                else
                {
                    //  Do nothing and move there.
                    target = null;
                    c = null;
                    chasing = false;
                }
            }

        //  Chase target.
        if (chasing)
        {
            //  In range.
            if (GLOBAL.HasReached(transform.position, target.position, AutoRange))
                AutoAttack();
            else
                //  Out of range.
                Invoke("MaintainDistance", .5f);
        }
    }

    void AutoAttack()
    {
        //  Fire an auto.
        GameObject auto = Instantiate(Auto, transform.position, Quaternion.identity);
        //  Set the auto's target and damage.
        auto.GetComponent<AutoProjectile>().Set(target, AutoDamage);

        //  Stop moving when in range.
        PlayerNav.StopMoving();
    }

    /// <summary>Move towards the target at maximum range.</summary>
    void MaintainDistance()
    {
        if (target != null)
            PlayerNav.MoveTo(target.position);
    }
}