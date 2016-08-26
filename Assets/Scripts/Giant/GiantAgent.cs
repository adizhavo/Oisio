using UnityEngine;
using System.Collections.Generic;

public class GiantAgent : MonoBehaviour, EventListener
{
    #region WorldEntity implementation

    public Vector3 WorlPos
    {
        get
        {
            return transform.position;
        }
        set
        {
            agent.SetDestination(value);
        }
    }

    #endregion

    public NavMeshAgent agent;
    public MapBlockHolder resourcesBlock;
    public float visibilityRadius;

    public GiantActionState currentState;
    // all available states will be inserted in this array
    public GiantActionState[] registeredState;

    protected virtual void Start ()
    {   
        resourcesBlock = new MapBlockHolder();

        InitStates();
        ChangeState<GiantIdleState>();
	}

    protected virtual void InitStates()
    {
        // all available states will be inserted in this array
        registeredState = new GiantActionState[]
            {
                new GiantIdleState(this)
                // next state
                // ...
            };
    }

    private void Update()
    {
        currentState.FrameFeed();
        ActionObserver.CheckForActions(this);
    }

    public virtual void ChangeState<T>() where T : GiantActionState
    {
        T state = GetActionState<T>();

        if (state != null) currentState = state;
        else  Debug.LogWarning("Current state is not mapped, changes will not apply");
    }

    protected virtual T GetActionState<T>() where T : GiantActionState
    {
        foreach(GiantActionState state in registeredState)
        {
            if (state is T) return (T)state;
        }

        return null;
    }

    #region EventListener implementation

    public float VisibilityRadius
    {
        get
        {
            return visibilityRadius;
        }
    }

    public void Notify(EventTrigger nearbyEvent)
    {
        currentState.Notify(nearbyEvent);
    }

    #endregion

    public virtual void GotoNearestResource()
    {
        WorlPos = resourcesBlock.GetNearestPosition(this);
    }

    public virtual void GotoRandomResource()
    {
        WorlPos = resourcesBlock.GetRandomPos();
    }
}
