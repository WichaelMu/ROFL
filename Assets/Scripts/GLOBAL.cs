using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLOBAL
{
    /// <summary>Gets the mouse position + 10.z.</summary>
    public static Vector3 GetMousePosition()
    {
        return new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
    }

    /// <summary>Gets the mouse position in screen coordinates.</summary>
    public static Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(GetMousePosition());
    }

    public static bool HoldLeftClick()
    {
        return Input.GetMouseButton(0);
    }

    public static bool LeftClick()
    {
        return Input.GetMouseButtonDown(0);
    }

    public static bool HoldRightClick()
    {
        return Input.GetMouseButton(1);
    }

    public static bool RightClick()
    {
        return Input.GetMouseButtonDown(1);
    }

    /// <returns>If the 'E' button was pressed.</returns>
    public static bool E()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    /// <returns>If the 'Q' button was pressed.</returns>
    public static bool Q()
    {
        return Input.GetKeyDown(KeyCode.Q);
    }

    /// <returns>If the 'W' button was pressed.</returns>
    public static bool W()
    {
        return Input.GetKeyDown(KeyCode.W);
    }

    /// <returns>If the 'R' button was pressed.</returns>
    public static bool R()
    {
        return Input.GetKeyDown(KeyCode.R);
    }

    public static bool HasReached(Vector3 from, Vector3 to, float limit = .1f)
    {
        return Vector3.Distance(from, to) <= limit;
    }

    /// <summary>
    /// Moves Rigidbody self towards Transform target at float velocity speed while turning at float TurnRadius.
    /// </summary>
    /// <param name="self">The Rigidbody who needs to follow Transform target.</param>
    /// <param name="target">The Transform target to follow.</param>
    /// <param name="velocity">The float velocity to travel towards Transform target.</param>
    /// <param name="TurnRadius">The float TurnRadius to change direction.</param>
    public static void Homing(Rigidbody self, Transform target, float velocity, float TurnRadius)
    {
        Transform _self = self.gameObject.transform;
        self.velocity = _self.forward * velocity;
        self.MoveRotation(Quaternion.RotateTowards(_self.rotation, Quaternion.LookRotation(target.position - _self.position), TurnRadius));
    }
}
