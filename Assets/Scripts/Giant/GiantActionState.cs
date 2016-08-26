using UnityEngine;

public abstract class GiantActionState
{
    protected GiantAgent giant;

    public GiantActionState(GiantAgent giant)
    {
        this.giant = giant;
    }

    public abstract void FrameFeed();
    public abstract void Notify(EventTrigger nerbyEvent);
}