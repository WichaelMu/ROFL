using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RangeFinder))]
public class Mouse : MonoBehaviour
{
    public Transform player;

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

        //GLOBAL.DrawLine(Vector3.right + new Vector3(transform.position.x - 7, transform.position.y, transform.position.z), Vector3.right + new Vector3(transform.position.x + 7, transform.position.y, transform.position.z), Color.green);
        GLOBAL.DrawLine((transform.right * -7) + transform.position, (transform.right * 7) + transform.position, Color.green);
    }

    void FixedUpdate()
    {
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    public void SetRange(float range)
    {
        this.range = range;
    }
}
