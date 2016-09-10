using UnityEngine;
using System.Collections.Generic;

public abstract class ConsumableAgent : Agent
{
    public abstract ConsumableType Item { get; }

    public enum ChargeState
    {
        Charged,
        Charging
    }

    public ChargeState ConsumableState
    {
        set; get;
    }

    #region implemented abstract members of Agent

    protected override List<AgentComponent> InitComponents()
    {
        return new List<AgentComponent>
        {
            new Chargable(this)  
        };
    }

    protected override AgentState[] InitStates()
    {
        return new AgentState[]
        {
            new NullAgentState()
        };
    }

    #endregion

    [Header("Agent configuration")]
    public float collectionTime;
    public float reloadTime;

    [HideInInspector] public float percentage = 1f;

    public virtual void Collect(Consumer cons)
    {
        Chargable c = RequestComponent<Chargable>();
        if (c != null) c.Consume(cons);
    }
}