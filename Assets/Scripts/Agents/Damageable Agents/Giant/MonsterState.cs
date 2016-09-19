using UnityEngine;

public abstract class MonsterState : AgentState
{
    protected MonsterAgent monster;

    public MonsterState(MonsterAgent monster)
    {
        this.monster = monster;
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