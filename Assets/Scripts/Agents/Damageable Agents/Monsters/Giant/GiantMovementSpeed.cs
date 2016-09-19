public enum SpeedLevel
{
    Slow, 
    Medium,
    Fast,
    Rage
}

[System.Serializable]
public struct GiantSpeed
{
    public SpeedLevel type;
    public float speed;
}