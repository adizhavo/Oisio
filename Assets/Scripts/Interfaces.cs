using UnityEngine;

#region Game interfaces

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

public interface EventListener : WorldEntity
{
    float VisibilityRadius {get;}
    void Notify(EventTrigger visibleAction);
}

public interface EventTrigger : WorldEntity
{
    int Priority {get;}
    EventSubject subject {get;}
}

public interface Chargable
{
    ChargableState current {get;}   
}

#endregion

#region Game enums

public enum EventSubject
{
    NerbyTarget,
    Attack
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

#endregion