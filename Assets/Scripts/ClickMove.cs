using System;
using UnityEngine;
using UnityEngine.AI;

/// <summary>Controls the movement of the player using mouse clicks.</summary>
public class ClickMove : MonoBehaviour
{
    /// <summary>The player's Nav Mesh Agent.</summary>
    NavMeshAgent player;
    Transform follow;

    void Start()
    {
        player = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        CheckForMouseInput();
        FollowCharacter();
    }

    /// <summary>Checks for a mouse input to move to.</summary>
    void CheckForMouseInput()
    {
        //  Move on right-click.
        if (Input.GetMouseButton(1))
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                Debug.DrawLine(Camera.main.transform.position, hit.point);

                if (hit.collider.CompareTag("Enemy"))
                {
                    follow = hit.collider.transform;
                    return;
                }
                MoveTo(hit.point);
                follow = null;
            }
    }

    void FollowCharacter()
    {
        if (follow == null)
            return;

        MoveTo(follow.position);

        if (Vector3.Distance(transform.position, follow.position) < .5f)
            StopMoving();
    }

    /// <param name="Destination">The Vector3 end position.</param>
    public void MoveTo(Vector3 Destination)
    {
        player.SetDestination(Destination);
    }

    public void StopMoving()
    {
        follow = null;
        MoveTo(transform.position);
    }
}