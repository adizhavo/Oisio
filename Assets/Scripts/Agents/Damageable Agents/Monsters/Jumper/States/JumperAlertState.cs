using UnityEngine;

public class JumperAlertState : GiantAlertState
{
    public JumperAlertState(MonsterAgent giant) : base (giant) { } 

    #region implemented abstract members of GiantState

    protected override void Init()
    {
        #if UNITY_EDITOR
        Debug.Log("Jumper enters into Alert state..");
        #endif

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
            eventPos = nerbyEvent.WorlPos;
            Attack();
        }
        else if (nerbyEvent.subject.Equals(EventSubject.SmokeBomb))
        {
            monster.ChangeState<GiantBlindState>(nerbyEvent);
        }
    }

    protected override void CheckAlertLevel()
    {
        if (monster.AlertLevel < GameConfig.minAlertLevel + Mathf.Epsilon)
        {
            monster.ChangeState<JumperIdleState>();
            ResetState();
        }
        else if (eventPos.HasValue && monster.AlertLevel >= GameConfig.maxAlertLevel - Time.deltaTime - Mathf.Epsilon)
        {
            Attack();
        }
    }

    #endregion

    private void Attack()
    {
        CustomEvent targetEvent = new CustomEvent(eventPos.Value, EventSubject.Attack, 100, 0f, true);
        monster.ChangeState<JumperAttackState>(targetEvent);
        ResetState();
    }
}
