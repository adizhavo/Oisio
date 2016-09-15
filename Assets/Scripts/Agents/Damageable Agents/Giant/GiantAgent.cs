using UnityEngine;
using System.Collections.Generic;

public class GiantAgent : DamageableAgent, EventListener
{
    [Header("Agent Configuration")]
    public GameObject attackGameObject;
    public GiantSpeed[] availableMovements;
    public float alertReactionSpeed;

    [Header("Attack Configuration")]
    public float attackDamage;
    public float attackRange;
    public float attackTime;
    public float visibilityRadius;

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
	}

    protected override List<AgentComponent> InitComponents()
    {
        return new List<AgentComponent>
        {
            new AttackAnimation(),
            new MapBlockHolder(),
            new AgentHealth(this),
            new GiantAttackComponent(this)
        };
    }

    protected override AgentState[] InitStates()
    {
        // all available states will be inserted in this array
        return new GiantState[]
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
        WorlPos = RequestComponent<MapBlockHolder>().GetNearestPosition(this);
    }

    public virtual void GotoRandomResource()
    {
        WorlPos = RequestComponent<MapBlockHolder>().GetRandomPos();
    }

    public void GotoLocalRandomPos()
    {
        Vector2 randomPoint = Random.insideUnitCircle * visibilityRadius;
        Vector3 movePos = new Vector3(randomPoint.x, 0f, randomPoint.y);
        WorlPos = movePos;
    }

    public virtual void PrepareAttack(float preparationTime)
    {
        RequestComponent<AttackAnimation>().PrepareAttack(attackGameObject, preparationTime);
    }

    public virtual void Attack()
    {
        RequestComponent<AttackAnimation>().Attack(attackGameObject);
        RequestComponent<GiantAttackComponent>().Attack<CharacterAgent>();
    }

    public virtual void RecoverAttack(float recoverTime)
    {
        RequestComponent<AttackAnimation>().Recover(attackGameObject, recoverTime);
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
