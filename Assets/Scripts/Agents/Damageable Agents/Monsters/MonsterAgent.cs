using UnityEngine;
using System.Collections.Generic;

public abstract class MonsterAgent : DamageableAgent, EventListener
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

    #region Agent implementation

    protected override void Update()
    {
        base.Update();

        EventObserver.SearchVisibleEvent(this);
        DrawGizmo();
    }

    #endregion

    public abstract void Attack();
    public abstract void PrepareAttack(float preparationTime);
    public abstract void RecoverAttack(float recoverTime);

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
