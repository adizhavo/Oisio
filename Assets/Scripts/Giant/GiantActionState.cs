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

    public virtual bool IsEventValid(GiantEvent requestedEvent)
    {
        foreach(GiantEvent e in subcribedEvent)
        {
            if (e.Equals(requestedEvent)) return true;
        }

        return false;
    }

    public virtual void Notify(GiantEvent firedEvent)
    {
        if (IsEventValid(firedEvent))
        {
            ChangeState(firedEvent);
        }
    }

    public abstract void FrameFeed();
    public abstract void ChangeState(GiantEvent firedEvent);
}

public enum GiantEvent
{
    TargetNerby,
}