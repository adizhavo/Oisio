using UnityEngine;
using System.Collections;

public class GiantHuntState : MonsterState 
{
    private Vector3? eventPos = null;

    public float reactionSpeed
    {
        get { return monster.alertReactionSpeed * Time.deltaTime; } 
    }

    public GiantHuntState(MonsterAgent monster) : base(monster) { }

    #region implemented abstract members of GiantActionState
    protected override void Init()
    {
        #if UNITY_EDITOR
        Debug.Log("Giant enters into Hunt state..");
        #endif

        monster.SetSpeed(SpeedLevel.Fast);
        eventPos = null;
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
        else if (nerbyEvent.subject.Equals(EventSubject.Attack))
        {
            monster.ChangeState<GiantRageState>(nerbyEvent);
        }
        else if (nerbyEvent.subject.Equals(EventSubject.SmokeBomb))
        {
            monster.ChangeState<GiantBlindState>(nerbyEvent);
        }
    }
    #endregion

    protected virtual void MovetoEventPos()
    {
        if (eventPos.HasValue)
        {
            monster.WorlPos = eventPos.Value;
        }
    }

    protected void CheckState()
    {
        if (!eventPos.HasValue)
        {
            monster.ChangeState<GiantAlertState>();
            Reset();
        }
        else
        {
            TrytoAttack();
        }
    }

    private void TrytoAttack()
    {
        float distance = Vector3.Distance(monster.WorlPos, eventPos.Value);
        if (distance < monster.attackRange)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        eventPos = null;
        monster.Stop();
        monster.ChangeState<GiantAttackState>();
    }

    private void Reset()
    {
        eventPos = null;
        monster.AlertLevel /= 2f;
    }
}