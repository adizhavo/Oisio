using UnityEngine;

#region Game interfaces

public interface WorldEntity
{
    Vector3 WorlPos {set; get;}
}

public interface Collectable : WorldEntity
{
    CollectableType type {get;}   
    void Gather(Collector collector);
}

public interface Collector
{
    void CompleteCollection(CollectableType collectable);
}

public interface EventListener : WorldEntity
{
    float VisibilityRadius {get;}
    void Notify(EventTrigger visibleAction);
}

public interface EventTrigger : WorldEntity
{
    bool oneShot {get;}
    bool hasExpired {get;}
    int Priority {get;}
    EventSubject subject {get;}
}

public interface Chargable
{
    ChargableState current {get;}   
}

public interface AgentComponent
{
    void FrameFeed();
}

public interface AgentState
{
    void Init(EventTrigger initialTrigger);
    void FrameFeed();
    void Notify(EventTrigger nerbyEvent);
}

#endregion

#region Game enums

public enum EventSubject
{
    NerbyTarget,
    Attack,
    SmokeBomb
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