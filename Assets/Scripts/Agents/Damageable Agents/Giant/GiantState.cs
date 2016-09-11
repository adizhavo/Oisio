using UnityEngine;

public abstract class GiantState : AgentState
{
    protected GiantAgent giant;

    public GiantState(GiantAgent giant)
    {
        this.giant = giant;
    }

    protected abstract void Init();

    #region AgentState implementation

    public virtual void Init(EventTrigger initialTrigger)
    {
        Init();
        if (initialTrigger != null) Notify(initialTrigger);
    }

    public abstract void FrameFeed();
    public abstract void Notify(EventTrigger nerbyEvent);

    #endregion
}