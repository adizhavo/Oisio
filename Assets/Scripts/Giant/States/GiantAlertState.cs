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
        #if UNITY_EDITOR
        Debug.Log("Giant enters into Alert state..");
        #endif

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
            RageToEvent(nerbyEvent);
        }
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
        CheckIdleState();
        CheckHuntState();
    }

    private void CheckIdleState()
    {
        if (giant.AlertLevel < GameConfig.minAlertLevel + Mathf.Epsilon)
        {
            giant.ChangeState<GiantIdleState>();
            ResetState();
        }
    }

    private void CheckHuntState()
    {
        if (giantState.Equals(AlertState.Hunt))
            return;
        if (giant.AlertLevel >= GameConfig.maxAlertLevel - Time.deltaTime - Mathf.Epsilon)
        {
            giantState = AlertState.Hunt;
            giant.SetSpeed(SpeedLevel.Fast);
        }
        else
        {
            giant.SetSpeed(SpeedLevel.Medium);
        }
    }

    private void RageToEvent(EventTrigger nerbyEvent)
    {
        eventPos = nerbyEvent.WorlPos;
        giant.AlertLevel = GameConfig.maxAlertLevel;
        giantState = AlertState.Hunt;
        giant.SetSpeed(SpeedLevel.Rage);
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
