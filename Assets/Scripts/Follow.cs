using UnityEngine;

public class Follow : MonoBehaviour
{

    public Transform player;

    public Vector3 offset;

    [Range(1, 100)]
    public int MouseDragSensitivity;

    bool CamLock = false;

    void Start()
    {
        FocusOnPlayer();
    }

    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            MoveCamera();
            return;
        }

        if (Input.GetKey(KeyCode.Space))
            FocusOnPlayer();

        if (Input.GetKeyDown(KeyCode.Y))
            CamLock = !CamLock;
    }

    void LateUpdate()
    {
        if (CamLock)
            FocusOnPlayer();
    }

    void FocusOnPlayer()
    {
        transform.position = player.position + offset;
    }

    void MoveCamera()
    {
        CamLock = false;
        transform.position += -(new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime, 0, Input.GetAxisRaw("Mouse Y") * Time.deltaTime) * MouseDragSensitivity);
    }
}