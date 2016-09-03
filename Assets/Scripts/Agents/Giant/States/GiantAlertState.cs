using UnityEngine;
using System.Collections;

public class GiantAlertState : GiantActionState 
{
    private Vector3? eventPos = null;

    public float reactionSpeed
    {
        get { return giant.alertReactionSpeed * Time.deltaTime; } 
    }

    public GiantAlertState(GiantAgent giant) : base(giant) 
    {
        giant.SetSpeed(SpeedLevel.Medium);
    }

    #region implemented abstract members of GiantActionState

    protected override void Init()
    {
        #if UNITY_EDITOR
        Debug.Log("Giant enters into Alert state..");
        #endif

        eventPos = null;
        IncreaseAlert();
    }

    public override void FrameFeed()
    {
        MovetoEventPos();
        DecreseAlert();
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
            giant.ChangeState<GiantRageState>(nerbyEvent);
        }
        else if (nerbyEvent.subject.Equals(EventSubject.SmokeBomb))
        {
            giant.ChangeState<GiantBlindState>(nerbyEvent);
        }
    }

    #endregion

    private void IncreaseAlert()
    {
        giant.AlertLevel += reactionSpeed * 2;
    }

    private void DecreseAlert()
    {
        giant.AlertLevel -= reactionSpeed;
    }

    protected virtual void MovetoEventPos()
    {
        if (eventPos.HasValue)
        {
            giant.WorlPos = eventPos.Value;
        }
    }

    private void CheckAlertLevel()
    {
        if (giant.AlertLevel < GameConfig.minAlertLevel + Mathf.Epsilon)
        {
            giant.ChangeState<GiantIdleState>();
            ResetState();
        }
        else if (giant.AlertLevel >= GameConfig.maxAlertLevel - Time.deltaTime - Mathf.Epsilon)
        {
            giant.ChangeState<GiantHuntState>();
        }
        else
        {
            giant.SetSpeed(SpeedLevel.Medium);
        }
    }

    private void ResetState()
    {
        if (eventPos.HasValue) giant.WorlPos = eventPos.Value;
        eventPos = null;
    }
}
