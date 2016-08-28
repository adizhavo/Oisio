using UnityEngine;

public abstract class GiantActionState
{
    protected GiantAgent giant;

    public GiantActionState(GiantAgent giant)
    {
        this.giant = giant;
    }

    public virtual void Init(EventTrigger initialTrigger)
    {
        Init();
        if (initialTrigger != null) Notify(initialTrigger);
    }

    protected abstract void Init();
    public abstract void FrameFeed();
    public abstract void Notify(EventTrigger nerbyEvent);
}