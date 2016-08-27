using UnityEngine;
using System.Collections;

public class GiantAlertState : GiantActionState 
{
    private Vector3? eventPos = null;

    private enum AlertState
    {
        Observe,
        Hunt
    }
    private AlertState giantState = AlertState.Observe;

    public float reactionSpeed
    {
        get { return giant.alertReactionSpeed * Time.deltaTime; } 
    }

    public GiantAlertState(GiantAgent giant) : base(giant) 
    {
        giantState = AlertState.Observe;
    }

    #region implemented abstract members of GiantActionState

    public override void Init()
    {
        IncreaseAlert();
    }

    public override void FrameFeed()
    {
        if (eventPos.HasValue)
        {
            if (giantState.Equals(AlertState.Hunt)) 
                TryAttack();

            MovetoEventPos();
        }

        giant.AlertLevel -= reactionSpeed;
        CheckAlertLevel();
        eventPos = null;
    }

    public override void Notify(EventTrigger nerbyEvent)
    {
        if (nerbyEvent.subject.Equals(EventSubject.NerbyTarget)) 
        {
            IncreaseAlert();
            eventPos = nerbyEvent.WorlPos;
        }
        else if (nerbyEvent.subject.Equals(EventSubject.Attack))
        {
            giant.AlertLevel = 1;
            eventPos = nerbyEvent.WorlPos;
        }

        Debug.Log(nerbyEvent.subject.ToString());
    }

    #endregion

    private void IncreaseAlert()
    {
        giant.AlertLevel += reactionSpeed * 2;
    }

    protected virtual void MovetoEventPos()
    {
        if (eventPos.HasValue)
        {
            giant.WorlPos = eventPos.Value;
        }
    }

    protected virtual void CheckAlertLevel()
    {
        if (giant.AlertLevel < Mathf.Epsilon)
        {
            giant.ChangeState<GiantIdleState>();
            ResetState();
        }
        else if (giant.AlertLevel >= 1 - Time.deltaTime - Mathf.Epsilon)
        {
            giantState = AlertState.Hunt;
            giant.SetSpeed(SpeedLevel.Fast);
        }
        else
        {
            giant.SetSpeed(SpeedLevel.Medium);
        }
    }

    protected void TryAttack()
    {
        float distance = Vector3.Distance(giant.WorlPos, eventPos.Value);
        if (distance < giant.AttackRange)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        eventPos = null;
        giant.ChangeState<GiantAttackState>();
        giant.Stop();
    }

    private void ResetState()
    {
        if (eventPos.HasValue) giant.WorlPos = eventPos.Value;
        giantState = AlertState.Observe;
        eventPos = null;
    }
}
