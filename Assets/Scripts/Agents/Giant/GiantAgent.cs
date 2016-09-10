using UnityEngine;
using System.Collections.Generic;

public class GiantAgent : Agent, EventListener
{
    [Header("Agent Configuration")]
    public GiantSpeed[] availableMovements;
    public float alertReactionSpeed;
    public float attackRange;
    public float attackTime;
    public float visibilityRadius;

    [Header("Health config")]
    public float maxHealth;
    public float healthRegen;

    [Header("Standart dependencies")]
    public AttackView areaDamageView;

    public MapBlockHolder resourcesBlock;
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

    #region Agent implementation

    protected override void Init ()
    {   
        base.Init();

        ChangeState<GiantIdleState>();
        resourcesBlock = new MapBlockHolder();
	}

    protected override List<AgentComponent> InitComponents()
    {
        return new List<AgentComponent>
        {
            new GiantHealth(this)
        };
    }

    protected override AgentState[] InitStates()
    {
        // all available states will be inserted in this array
        return new GiantActionState[]
            {
                new GiantIdleState(this),
                new GiantAlertState(this),
                new GiantAttackState(this),
                new GiantHuntState(this),
                new GiantRageState(this), 
                new GiantBlindState(this)
                // next state
                // ...
            };
    }

    protected override void Update()
    {
        base.Update();

        EventObserver.SearchVisibleEvent(this);
        DrawGizmo();
    }

    #endregion

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
                navMeshAgent.speed = sp.speed;
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

    public virtual void PrepareAttack(float preparationTime)
    {
        areaDamageView.PrepareAttack(preparationTime);
    }

    public virtual void Attack()
    {
        Debug.Log("Giant attacks");
    }

    public virtual void RecoverAttack(float recoverTime)
    {
        areaDamageView.Recover(recoverTime);
    }

    public void Stop()
    {
        WorlPos = transform.position;
    }

    private void DrawGizmo()
    {
        #if UNITY_EDITOR

        Debug.DrawLine(transform.position, transform.position + transform.right.normalized * attackRange, Color.red);
        Debug.DrawLine(transform.position + transform.forward/5, transform.position + transform.forward/5 + transform.right.normalized * VisibilityRadius, Color.blue);

        #endif
    }
}
