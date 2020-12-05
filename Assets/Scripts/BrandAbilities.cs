using System.Collections.Generic;
using UnityEngine;

public class BrandAbilities : MonoBehaviour
{

    public GameObject AbilityQ;
    Character character;
    RangeFinder RF;

    public const int QRANGE = 7;
    public const int WRANGE = 5;
    public const int ERANGE = 10;
    public const int RRANGE = 15;

    char selected;

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

        Transform target = null;

        if (GLOBAL.LeftClickHit())
        {
            RaycastHit h = GLOBAL.MousePosRay();
            if (h.collider.GetComponent<Character>() != null)
                target = h.transform;
        }

        if (target != null && GLOBAL.HasReached(transform.position, target.position, BrandAbilities.RRANGE))
        {
            GameObject shot = Instantiate(AbilityQ, transform.position, Quaternion.identity);
            shot.GetComponent<BrandR>().Set(target, 100);
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
}
