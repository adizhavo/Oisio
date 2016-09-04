using UnityEngine;

public class GiantIdleState : GiantActionState
{
    protected float waitTime;

    public GiantIdleState(GiantAgent giant) : base(giant) { }

    #region implemented abstract members of GiantActionState
    protected override void Init()
    {
        #if UNITY_EDITOR
        Debug.Log("Giant enters into Idle state..");
        #endif

        waitTime = Random.Range(3f, 7f);
        giant.SetSpeed(SpeedLevel.Slow);
    }

    public override void FrameFeed()
    {
        if (waitTime < 0f)
        {
            giant.GotoNearestResource();
            waitTime = Random.Range(3f, 7f);

            if (ShouldChangeResource())
            {
                giant.GotoRandomResource();
                waitTime += 5f;
            }
        }

        waitTime -= Time.deltaTime;
    }

    public override void Notify(EventTrigger nerbyEvent)
    {
        if (nerbyEvent.subject.Equals(EventSubject.NerbyTarget) || nerbyEvent.subject.Equals(EventSubject.Attack)) 
        {
            giant.ChangeState<GiantAlertState>(nerbyEvent);
        }
    }

    #endregion

    protected virtual bool ShouldChangeResource()
    {
        return Random.Range(0, 2) == 0;
    }
}