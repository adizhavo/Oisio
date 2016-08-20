using UnityEngine;

public interface Collectable
{
    CollectableType type {get;}   
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