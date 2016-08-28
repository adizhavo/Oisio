using UnityEngine;
using System.Collections;

public class GiantBlindState : GiantActionState 
{
    private Vector3? eventPos;

    public GiantBlindState(GiantAgent giant) : base(giant) { }

    #region implemented abstract members of GiantActionState
    protected override void Init()
    {
        #if UNITY_EDITOR
        Debug.Log("Giant enters into Blind state..");
        #endif

        eventPos = null;
    }

    public override void FrameFeed()
    {
        CheckDistance();
        DrawEscapePos();
    }

    public override void Notify(EventTrigger nerbyEvent)
    {
        if (!eventPos.HasValue && nerbyEvent.subject.Equals(EventSubject.SmokeBomb))
        {
            Escape(nerbyEvent);
        }
    }
    #endregion

    private void CheckDistance()
    {
        if (!eventPos.HasValue) return;

        float distance = (int)(Vector3.Distance(eventPos.Value, giant.WorlPos) * 10);
        if (distance  < Mathf.Epsilon)
        {
            giant.ChangeState<GiantAlertState>();
        }
    }
    
    private void Escape(EventTrigger nerbyEvent)
    {
        Vector3 bombDirection = (giant.agent.pathEndPosition - giant.WorlPos).normalized;
        bombDirection.y = 0;

        eventPos = nerbyEvent.WorlPos + bombDirection * GameConfig.giantEscapeDistance;
        giant.WorlPos = eventPos.Value;
        giant.SetSpeed(SpeedLevel.Fast);
    }

    private void DrawEscapePos()
    {
        #if UNITY_EDITOR

        if (eventPos.HasValue) Debug.DrawLine(giant.WorlPos, eventPos.Value, Color.red);

        #endif
    }
}