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
    public GiantSpeed[] availableMovements;

    public AttackView areDamageView;

    public MapBlockHolder resourcesBlock;
    public float visibilityRadius;
    [Range(0f, 0.5f)] public float alertReactionSpeed;
    public float AttackRange;
    public float AttackTime;

    private float alertLevel;
    public float AlertLevel
    {
        get 
        {
            return alertLevel;
        }
        set
        {
            alertLevel = Mathf.Clamp01(value);
        }
    }

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
                new GiantIdleState(this),
                new GiantAlertState(this),
                new GiantAttackState(this)
                // next state
                // ...
            };
    }

    private void Update()
    {
        currentState.FrameFeed();
        EventObserver.CheckforEvent(this);

        DrawGizmo();
    }

    public virtual void ChangeState<T>() where T : GiantActionState
    {
        T state = GetActionState<T>();

        if (state != null) 
        {
            currentState = state;
            currentState.Init();
        }
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
        #if UNITY_EDITOR
        Debug.Log("Giant notified with subject: " + nearbyEvent.subject.ToString());
        #endif

        currentState.Notify(nearbyEvent);
    }

    #endregion

    public void SetSpeed(SpeedLevel level)
    {
        foreach(GiantSpeed sp in availableMovements)
        {
            if (sp.type.Equals(level))
            {
                agent.speed = sp.speed;
                return;
            }
        }

        Debug.Log(level.ToString() + " speed level is not supported");
    }

    public virtual void GotoNearestResource()
    {
        WorlPos = resourcesBlock.GetNearestPosition(this);
    }

    public virtual void GotoRandomResource()
    {
        WorlPos = resourcesBlock.GetRandomPos();
    }

    public void GotoLocalRandomPos()
    {
        Vector2 randomPoint = Random.insideUnitCircle * visibilityRadius;
        Vector3 movePos = new Vector3(randomPoint.x, 0f, randomPoint.y);
        WorlPos = movePos;
    }

    public virtual void Attack(float attackTime)
    {
        areDamageView.Attack(attackTime);
    }

    public virtual void RecoverAttack(float recoverTime)
    {
        areDamageView.Recover(recoverTime);
    }

    public void Stop()
    {
        WorlPos = transform.position;
    }

    private void DrawGizmo()
    {
        #if UNITY_EDITOR

        Debug.DrawLine(transform.position, transform.position + transform.right.normalized * AttackRange, Color.red);
        Debug.DrawLine(transform.position + transform.forward/5, transform.position + transform.forward/5 + transform.right.normalized * VisibilityRadius, Color.blue);

        #endif
    }
}
