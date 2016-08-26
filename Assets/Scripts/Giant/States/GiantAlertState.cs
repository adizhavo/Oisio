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

    public GiantAlertState(GiantAgent giant) : base(giant) 
    {
        giantState = AlertState.Observe;
    }

    #region implemented abstract members of GiantActionState

    public override void FrameFeed()
    {
        if (giantState.Equals(AlertState.Hunt))
        {
            MovetoEventPos();
            TryAttack();
        }

        CheckAlertLevel();
        giant.AlertLevel -= giant.alertReactionSpeed * Time.deltaTime;
    }

    public override void Notify(EventTrigger nerbyEvent)
    {
        if (nerbyEvent.subject.Equals(EventSubject.NerbyTarget)) 
        {
            giant.AlertLevel += giant.alertReactionSpeed * Time.deltaTime * 2;
            eventPos = nerbyEvent.WorlPos;
        }
    }

    #endregion

    protected virtual void MovetoEventPos()
    {
        if (eventPos.HasValue)
        {
            giant.WorlPos = eventPos.Value;
        }
    }

    protected virtual void CheckAlertLevel()
    {
        if (giant.AlertLevel >= 1 + Mathf.Epsilon)
        {
            giantState = AlertState.Hunt;
        }
        else if (giant.AlertLevel < Mathf.Epsilon)
        {
            giant.ChangeState<GiantIdleState>();
            giant.GotoLocalRandomPos();
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
        // Change to attack state
    }
}
