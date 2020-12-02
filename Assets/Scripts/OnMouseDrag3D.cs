using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseDrag3D : MonoBehaviour
{
    public Material ChangeTo;

    MeshRenderer rend;
    Material Default;

    void Awake()
    {
        rend = GetComponent<MeshRenderer>();
        Default = rend.material;
    }

    void OnMouseDrag()
    {
        Vector3 MWP = GLOBAL.GetMouseWorldPosition();
        transform.position = new Vector3(Mathf.RoundToInt(MWP.x), Mathf.RoundToInt(MWP.y), Mathf.RoundToInt(MWP.z));

        if (transform.position == new Vector3(5, 5, 0))
            rend.material = ChangeTo;
        else
            rend.material = Default;
    }
}