using UnityEngine;

public interface WorldEntity
{
    Vector3 WorlPos {get;}
}

public interface Collectable : WorldEntity
{
    CollectableType type {get;}   
    void Collect();
}

public interface Collector : WorldEntity
{
    void Notify(Collectable nerbyCollectable);
}

public interface Chargable
{
    ChargableState current {get;}   
}

public enum ChargableState
{
    Charged,
    Charging
}

public enum CollectableType
{
    Arrow,
    Bomb
}