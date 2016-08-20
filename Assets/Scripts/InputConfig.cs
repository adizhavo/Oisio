using UnityEngine;
using System.Collections;

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
}