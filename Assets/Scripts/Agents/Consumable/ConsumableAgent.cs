using UnityEngine;
using System.Collections.Generic;

public abstract class ConsumableAgent : Agent
{
    public abstract ConsumableType type { get; }

    public enum ChargableState
    {
        Charged,
        Charging
    }

    public ChargableState current
    {
        set; get;
    }

    #region implemented abstract members of Agent
    public Vector3 WorlPos
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        }
    }

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

    public virtual void Consume(Consumer collector)
    {
        Chargable c = RequestComponent<Chargable>();
        if (c != null) c.Consume(collector);
    }
}