using System.Collections.Generic;
using UnityEngine;

public class BrandAbilities : MonoBehaviour
{

    public GameObject AbilityQ;
    Character character;

    void Start()
    {
        character = GetComponent<Character>();
    }

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

    }

    void W()
    {

    }

    void E()
    {

    }

    void R()
    {
        if (character.target != null)
        {
            GameObject shot = Instantiate(AbilityQ, transform.position, Quaternion.identity);
            shot.GetComponent<BrandQ>().Set(character.target, 100);
        }
    }
}
