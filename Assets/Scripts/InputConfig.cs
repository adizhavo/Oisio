using UnityEngine;

public static class InputConfig 
{
    public static float XDriection()
    {
        return Input.GetAxis("Horizontal");
    }

    public static float YDriection()
    {
        return Input.GetAxis("Vertical");
    }

    public static bool Run()
    {
        return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }

    public static bool Aim()
    {
        return Input.GetKey(KeyCode.Space);
    }

    public static bool ActionDown()
    {
        return Input.GetMouseButtonDown(0);
    }

    public static bool Action()
    {
        return Input.GetMouseButton(0);
    }

    public static bool ActionUp()
    {
        return Input.GetMouseButtonUp(0);
    }

    public static Vector3 CursorPosition()
    {
        return Input.mousePosition;
    }

    public static Vector2 GetCursorMovement()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
}