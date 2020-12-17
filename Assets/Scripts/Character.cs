using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(ClickMove))]
[RequireComponent(typeof(Shop))]
public class Character : MonoBehaviour
{
    public GameObject Auto;
    Image HealthBar;
    public TextMeshProUGUI NameBox;
    public TextMeshProUGUI MoneyBox;

    [HideInInspector]
    public Transform target;
    ClickMove PlayerNav;
    Shop shop;

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

    bool chasing = false;

    void Start()
    {
        PlayerNav = GetComponent<ClickMove>();

        HealthBar = GetComponentInChildren<Image>();

        NameBox.text = _name;

        UpdateMoney(money);

        Spawn();

        GLOBAL.PlayingAs = this;

        shop = GetComponent<Shop>();
        if (shop)
            shop.ShowHide(false);
    }

    void Update()
    {
        BasicAttack();

        Keyboard();
    }

    void Keyboard()
    {
        if (GLOBAL.X())
            shop.ShowHide();
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
            GetComponent<Renderer>().material.color = Color.red;

        HealthBar.fillAmount = CurrentHealth / MaxHealth;

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

    public void Buy(Item i)
    {
        if (i.price <= money)
        {
            shop.items.Add(i);
            UpdateMoney(money -= i.price);

            shop.PrintItems();
            UpdateItemAttributes(i, true);
        }
    }

    public void Sell(Item i)
    {
        if (shop.HasItem(i))
        {
            shop.items.Remove(i);

            UpdateMoney(money += i.SellPrice());
            UpdateItemAttributes(i, false);
        }
        else
        {
            Debug.LogWarning(_name + " tried to sell " + i._name + " but cannot.");
            shop.PrintItems();
        }

    }

    public void Refund(Item i)
    {
        shop.items.Remove(i);

        ///  IF AND ONLY IF CHARACTER HAS NOT LEFT THE BUYING AREA.
        if (shop.HasItem(i))
        {
            UpdateMoney(money += i.price);
        }
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
                if (GLOBAL.CheckCharacter(this, c))
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
                Invoke(nameof(MaintainDistance), .5f);
        }
    }

    void AutoAttack()
    {
        transform.LookAt(target);

        //  Fire an auto.
        GameObject auto = Instantiate(Auto, transform.position, transform.rotation);
        //  Set the auto's target and damage.
        auto.GetComponent<AutoProjectile>().Set(target, AutoDamage);

        //  Stop moving when in range.
        if (PlayerNav == null)
            throw new NullReferenceException("PlayerNav is set to null for: " + _name);
        else
            PlayerNav.StopMoving();
    }

    /// <summary>Move towards the target at maximum range.</summary>
    void MaintainDistance()
    {
        if (!PlayerNav)
            throw new NullReferenceException("PlayerNav is set to null for: " + _name);
        else
            if (target != null)
                PlayerNav.MoveTo(target.position);
    }

    void UpdateMoney(int NewMoney) => MoneyBox.text = "$" + NewMoney.ToString();

    /// <summary>
    /// Updates this character's attributes.
    /// </summary>
    /// <param name="i">The item that is being used to update.</param>
    /// <param name="buying">If this character is buying.</param>
    void UpdateItemAttributes(Item i, bool buying)
    {
        if (buying)
        {
            MaxHealth   +=  i._health;
            AutoDamage  +=  i._damage;
        }
        else
        {
            MaxHealth   -=  i._health;
            AutoDamage  -=  i._damage;
        }
    }

}