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

public interface ActionListener : WorldEntity
{
    float VisibilityRadius {get;}
    void Notify(SceneEvent action);
}

public interface Action : WorldEntity
{
    int Priority {get;}
    SceneEvent actionEvent {get;}
}

public interface Chargable
{
    ChargableState current {get;}   
}

#endregion

#region Game enums

public enum SceneEvent
{
    NerbyTarget,
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