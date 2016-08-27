public enum SpeedLevel
{
    Slow, 
    Medium,
    Fast
}

[System.Serializable]
public struct GiantSpeed
{
    public SpeedLevel type;
    public float speed;
}