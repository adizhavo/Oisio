using UnityEngine;
using System.Collections;

public static class GameConfig 
{
    public static readonly float maxAlertLevel = 1;
    public static readonly float minAlertLevel = 0;

    public static float giantEscapeDistance = 2;

    public static readonly int projectilePriority = 2;
    public static readonly int characterPriority = 10;
    public static readonly int smokeBombPriotity = 100;

    public static readonly float smokeBombEnableTime = 10;
    public static readonly float smokeBombRange = 0.2f;
}
