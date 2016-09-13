using UnityEngine;

public class JumperAlertState : GiantAlertState
{
    public JumperAlertState(GiantAgent giant) : base (giant) { } 

    #region implemented abstract members of GiantState

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
    }

    #endregion
}
