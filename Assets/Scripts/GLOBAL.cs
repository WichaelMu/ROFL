using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLOBAL
{

    public static Character PlayingAs;

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

    /// <returns>If left click is being held down.</returns>
    public static bool HoldLeftClick()
    {
        return Input.GetMouseButton(0);
    }

    /// <returns>If left click was pressed.</returns>
    public static bool LeftClick()
    {
        return Input.GetMouseButtonDown(0);
    }

    /// <returns>If right click is being held down.</returns>
    public static bool HoldRightClick()
    {
        return Input.GetMouseButton(1);
    }

    /// <returns>If right click was pressed.</returns>
    public static bool RightClick()
    {
        return Input.GetMouseButtonDown(1);
    }

    /// <returns>If the 'A' button was pressed.</returns>
    public static bool A()
    {
        return Input.GetKeyDown(KeyCode.A);
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

    /// <summary>
    /// Determine if the distance between Vector3 from and Vector3 to is less than or equal to float limit.
    /// </summary>
    /// <param name="from">Vector3 location.</param>
    /// <param name="to">Vector3 location to compare to Vector3 first location.</param>
    /// <param name="limit">Defines if Vector3 from and Vector3 to have reached each other.</param>
    /// <returns>True if Vector3 from and Vector3 to are within float limit.</returns>
    public static bool HasReached(Vector3 from, Vector3 to, float limit = .1f)
    {
        return Vector3.Distance(from, to) <= limit;
    }

    /// <summary>
    /// Moves Rigidbody self towards Transform target at float velocity speed while turning at float TurnRadius.
    /// Use this in FixedUpdate().
    /// </summary>
    /// <param name="self">The Rigidbody who needs to follow Transform target.</param>
    /// <param name="target">The Transform target to follow.</param>
    /// <param name="velocity">The float velocity to travel towards Transform target.</param>
    /// <param name="TurnRadius">The float TurnRadius to change direction.</param>
    public static void Homing(Rigidbody self, Transform target, float velocity, float TurnRadius)
    {
        Transform _self = self.transform;
        self.velocity = _self.forward * velocity;
        self.MoveRotation(Quaternion.RotateTowards(_self.rotation, Quaternion.LookRotation(target.position - _self.position), TurnRadius));
    }

    /// <returns>True if a raycast hit something when LMB is pressed.</returns>
    public static bool LeftClickHit()
    {
        return Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition)) && Input.GetMouseButtonDown(0);
    }

    /// <returns>The raycast hit information.</returns>
    public static RaycastHit MousePosRay()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);

        return hit;
    }

    /// <summary>
    /// Draws a line from Vector3 start to Vector3 end in Color color for float duration.
    /// </summary>
    /// <param name="start">Vector3 coordinates of where the line will begin.</param>
    /// <param name="end">Vector3 coordinates of where the line will stop.</param>
    /// <param name="color">The Colour color of the line.</param>
    public static void DrawLine(Vector3 start, Vector3 end, Color color, float width = .1f, float MaxDistance = Mathf.Infinity)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer LR = myLine.GetComponent<LineRenderer>();
        LR.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
        LR.startColor = color;
        LR.endColor = color;
        LR.startWidth = width;
        LR.endWidth = width;
        LR.SetPosition(0, start);

        if (MaxDistance != Mathf.Infinity) {
            Vector3 d = end - start;
            float dist = Mathf.Clamp(Vector3.Distance(start, end), 0, MaxDistance);
            end = start + (d.normalized * dist);
        }

        LR.SetPosition(0, start);
        LR.SetPosition(1, end);
        GameObject.Destroy(myLine, Time.fixedDeltaTime);
    }

    /// <param name="self"></param>
    /// <param name="comparable"></param>
    /// <returns>If Character self is not Character comparable and Character comparable is not null.</returns>
    public static bool CheckCharacter(Character self, Character comparable)
    {
        return self != comparable && comparable != null;
    }

    public static bool X()
    {
        return Input.GetKeyDown(KeyCode.X);
    }
}