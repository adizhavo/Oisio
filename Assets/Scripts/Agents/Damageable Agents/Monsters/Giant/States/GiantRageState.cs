using UnityEngine;
using Oisio.Agent;

public class GiantRageState : MonsterState
{
    private Vector3? eventPos = null;

    public GiantRageState(MonsterAgent monster) : base(monster) { }

    #region implemented abstract members of GiantActionState

    protected override void Init()
    {
        #if UNITY_EDITOR
        Debug.Log("Giant enters into Rage state..");
        #endif

        monster.SetSpeed(SpeedLevel.Rage);
        monster.AlertLevel = GameConfig.maxAlertLevel;
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

    private void CheckAttack()
    {
        if (!eventPos.HasValue) return;

        float distance = Vector3.Distance(monster.WorlPos, eventPos.Value);
        if (distance < monster.attackRange)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        monster.Stop();
        eventPos = null;
        monster.ChangeState<GiantAttackState>();
    }

    private void RageToEvent(EventTrigger nerbyEvent)
    {
        Init();
        eventPos = nerbyEvent.WorlPos;
    }
}