using UnityEngine;
using System.Collections;

public class GiantRageState : GiantActionState
{
    private Vector3? eventPos = null;

    public GiantRageState(GiantAgent giant) : base(giant) { }

    #region implemented abstract members of GiantActionState

    protected override void Init()
    {
        #if UNITY_EDITOR
        Debug.Log("Giant enters into Rage state..");
        #endif

        giant.SetSpeed(SpeedLevel.Rage);
        giant.AlertLevel = GameConfig.maxAlertLevel;
    }

    public override void FrameFeed()
    {
        MovetoEventPos();
        CheckAttack();
    }

    public override void Notify(EventTrigger nerbyEvent)
    {
        if (nerbyEvent.subject.Equals(EventSubject.Attack) || nerbyEvent.subject.Equals(EventSubject.NerbyTarget))
        {
            RageToEvent(nerbyEvent);
        }
        else if (nerbyEvent.subject.Equals(EventSubject.SmokeBomb))
        {
            giant.ChangeState<GiantBlindState>(nerbyEvent);
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

    private void CheckAttack()
    {
        if (!eventPos.HasValue) return;

        float distance = Vector3.Distance(giant.WorlPos, eventPos.Value);
        if (distance < giant.AttackRange)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        giant.Stop();
        eventPos = null;
        giant.ChangeState<GiantAttackState>();
    }

    private void RageToEvent(EventTrigger nerbyEvent)
    {
        Init();
        eventPos = nerbyEvent.WorlPos;
    }
}