using UnityEngine;

public class JumperAlertState : GiantAlertState
{
    public JumperAlertState(GiantAgent giant) : base (giant) { } 

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
    }

    protected override void CheckAlertLevel()
    {
        if (giant.AlertLevel < GameConfig.minAlertLevel + Mathf.Epsilon)
        {
            giant.ChangeState<JumperIdleState>();
            ResetState();
        }
        else if (eventPos.HasValue && giant.AlertLevel >= GameConfig.maxAlertLevel - Time.deltaTime - Mathf.Epsilon)
        {
            CustomEvent targetEvent = new CustomEvent(eventPos.Value, EventSubject.Attack, 100, 0f, true);
            giant.ChangeState<JumperAttackState>(targetEvent);
            ResetState();
        }
    }

    #endregion
}
