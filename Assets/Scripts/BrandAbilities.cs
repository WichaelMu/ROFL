using System;
using System.Collections.Generic;
using UnityEngine;

public class BrandAbilities : MonoBehaviour
{

    public GameObject AbilityQ;
    Character character;
    RangeFinder RF;

    Transform target;

    public const int QRANGE = 7;
    public const int WRANGE = 5;
    public const int ERANGE = 10;
    public const int RRANGE = 15;

    char selected;
    bool chasing = false;

    void Start()
    {
        character = GetComponent<Character>();
        RF = GetComponent<RangeFinder>();
    }

    void Update()
    {
        if (GLOBAL.A() || selected == 'A')
            A();
        if (GLOBAL.Q() || selected == 'Q')
            Q();
        if (GLOBAL.W() || selected == 'W')
            W();
        if (GLOBAL.E() || selected == 'E')
            E();
        if (GLOBAL.R() || selected == 'R')
            R();
        if (GLOBAL.RightClick())
            ClearRange();

        CheckChase();
    }

    void A()
    {
        ClearRange();
        selected = 'A';
        ShowRange(character.AutoRange);
        GLOBAL.DrawLine(transform.position, RF.Mouse.transform.position, Color.red, .1f, character.AutoRange);
    }

    void Q()
    {
        ClearRange();
        selected = 'Q';
        ShowRange(QRANGE);
        GLOBAL.DrawLine(transform.position, RF.Mouse.transform.position, Color.red, .1f, QRANGE);
    }

    void W()
    {
        ClearRange();
        selected = 'W';
        ShowRange(WRANGE);
        RF.Mouse.SetRange(3);
        GLOBAL.DrawLine(transform.position, RF.Mouse.transform.position, Color.red, .1f, WRANGE);
    }

    void E()
    {
        ClearRange();
        selected = 'E';
        ShowRange(ERANGE);
        GLOBAL.DrawLine(transform.position, RF.Mouse.transform.position, Color.red, .1f, ERANGE);
    }

    void R()
    {
        ClearRange();
        selected = 'R';
        ShowRange(RRANGE);
        GLOBAL.DrawLine(transform.position, RF.Mouse.transform.position, Color.red, .1f, RRANGE);

        if (GLOBAL.LeftClickHit())
        {
            RaycastHit h = GLOBAL.MousePosRay();
            Character c = h.collider.GetComponent<Character>();
            if (GLOBAL.CheckCharacter(character, c))
            {
                target = h.transform;
                character.GetPlayerNav().MoveTo(c.transform.position);
                chasing = true;
            }
            else
            {
                target = null;
                c = null;
                chasing = false;
            }
        }

        if (target != null && GLOBAL.HasReached(transform.position, target.position, BrandAbilities.RRANGE))
        {
            Shoot(selected);
            character.GetPlayerNav().StopMoving();
        }
    }

    /// <summary>Shows a range on this character.</summary>
    /// <param name="range">Float range to show.</param>
    void ShowRange(float range)
    {
        RF.DoRenderer(range);
    }

    /// <summary>Clears the range on the mouse.</summary>
    void ClearRange()
    {
        selected = ' ';
        ShowRange(0);
        RF.Mouse.SetRange(0);
    }

    void CheckChase()
    {
        if (chasing)
            if (target != null)
                switch (selected)
                {
                    case 'Q':
                        if (GLOBAL.HasReached(transform.position, target.position, BrandAbilities.QRANGE))
                            Shoot(selected);
                        break;
                    case 'W':
                        if (GLOBAL.HasReached(transform.position, target.position, BrandAbilities.WRANGE))
                            Shoot(selected);
                        break;
                    case 'E':
                        if (GLOBAL.HasReached(transform.position, target.position, BrandAbilities.ERANGE))
                            Shoot(selected);
                        break;
                    case 'R':
                        if (GLOBAL.HasReached(transform.position, target.position, BrandAbilities.RRANGE))
                            Shoot(selected);
                        break;
                }
    }

    void Shoot(char selected)
    {
        switch (selected)
        {
            case 'Q':
                break;
            case 'W':
                break;
            case 'E':
                break;
            case 'R':
                GameObject shot = Instantiate(AbilityQ, transform.position, transform.rotation);
                shot.GetComponent<BrandR>().Set(target, 100);
                target = null;
                break;
        }
    }
}
