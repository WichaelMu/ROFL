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

    public static bool E()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    public static bool Q()
    {
        return Input.GetKeyDown(KeyCode.Q);
    }

    public static bool W()
    {
        return Input.GetKeyDown(KeyCode.W);
    }

    public static bool R()
    {
        return Input.GetKeyDown(KeyCode.R);
    }
}
