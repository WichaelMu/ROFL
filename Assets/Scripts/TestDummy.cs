using UnityEngine;

public class TestDummy : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position += new Vector3(-.05f, 0, 0);
    }
}