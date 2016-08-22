using UnityEngine;

public interface WorldEntity
{
    Vector3 WorlPos {set; get;}
}

public interface Collectable : WorldEntity
{
    CollectableType type {get;}   
    void Collect(Collector collector);
}

public interface Collector : WorldEntity
{
    void CompleteCollection(CollectableType collectable);
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