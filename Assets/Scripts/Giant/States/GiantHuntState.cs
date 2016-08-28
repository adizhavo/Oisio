using UnityEngine;
using System.Collections;

public class GiantHuntState : GiantActionState 
{
    private Vector3? eventPos = null;

    public float reactionSpeed
    {
        get { return giant.alertReactionSpeed * Time.deltaTime; } 
    }

    public GiantHuntState(GiantAgent giant) : base(giant) { }

    #region implemented abstract members of GiantActionState
    public override void Init()
    {
        #if UNITY_EDITOR
        Debug.Log("Giant enters into Hunt state..");
        #endif

        giant.SetSpeed(SpeedLevel.Fast);
    }

    public override void FrameFeed()
    {
        MovetoEventPos();
        CheckState();
        eventPos = null;
    }

    public override void Notify(EventTrigger nerbyEvent)
    {
        if (nerbyEvent.subject.Equals(EventSubject.NerbyTarget)) 
        {
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

    protected void CheckState()
    {
        if (!eventPos.HasValue)
        {
            giant.ChangeState<GiantAlertState>();
            Reset();
        }
        else
        {
            TrytoAttack();
        }
    }

    void TrytoAttack()
    {
        float distance = Vector3.Distance(giant.WorlPos, eventPos.Value);
        if (distance < giant.AttackRange)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        Reset();
        giant.Stop();
        giant.ChangeState<GiantAttackState>();
    }

    private void Reset()
    {
        eventPos = null;
        giant.AlertLevel /= 2f;
    }
}