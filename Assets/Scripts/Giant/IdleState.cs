using UnityEngine;

public class IdleState : GiantActionState
{
    protected float waitTime = 0f;

    public IdleState(GiantAgent giant, params GiantEvent[] events) : base(giant, events) { }

    #region implemented abstract members of GiantActionState
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

    public override void ChangeState(GiantEvent firedEvent)
    {
        // Change giant state
        // if (firedEvent.Equals(GiantEvent.NerbyTarget)) 
        // giant.ChangeState<Alert>()
    }

    #endregion

    protected virtual bool ShouldChangeResource()
    {
        return Random.Range(0, 2) == 0;
    }
}