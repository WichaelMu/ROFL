using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RangeFinder))]
public class Mouse : MonoBehaviour
{
    RangeFinder RF;
    Vector3 offset = new Vector3(0, .25f, 0);
    float range;

    void Start()
    {
        RF = GetComponent<RangeFinder>();
    }

    void Update()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(r, out RaycastHit hit, 5000))
        {
            transform.position = hit.point + offset;
            RF.DoRenderer(range);
        }
    }

    public void SetRange(float range)
    {
        this.range = range;
    }
}
