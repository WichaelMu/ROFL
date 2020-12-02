using System.Collections.Generic;
using UnityEngine;

public class BrandAbilities : MonoBehaviour
{

    public GameObject AbilityQ;

    void Update()
    {
        if (GLOBAL.Q())
            Q();

        if (GLOBAL.W())
            W();

        if (GLOBAL.E())
            E();

        if (GLOBAL.R())
            R();
    }

    void Q()
    {
        //GameObject shot = Instantiate(AbilityQ, transform.position, Quaternion.LookRotation());

    }

    void W()
    {

    }

    void E()
    {

    }

    void R()
    {

    }
}
