using UnityEngine;

public abstract class GiantActionState
{
    protected GiantEvent[] subcribedEvent;
    protected GiantAgent giant;

    public GiantActionState(GiantAgent giant, params GiantEvent[] subcribedEvent)
    {
        this.giant = giant;
        this.subcribedEvent =subcribedEvent;
    }

    public abstract void FrameFeed();
    public abstract void Notify(GiantEvent firedEvent);
}

public enum GiantEvent
{
    TargetNerby,
}