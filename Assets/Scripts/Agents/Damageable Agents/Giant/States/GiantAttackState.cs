using UnityEngine;
using System.Collections;

public class GiantAttackState : GiantState
{
    protected float attackEventPercentage = 0.7f;

    protected bool stopFrameFeed = false;
    protected float timeElapse;

    public GiantAttackState(GiantAgent giant) : base(giant) { }

    #region implemented abstract members of GiantActionState
    protected override void Init()
    {
        #if UNITY_EDITOR
        Debug.Log("Giant enters into Attack state..");
        #endif

        attackEventPercentage = Mathf.Clamp01(attackEventPercentage);
        giant.PrepareAttack(giant.attackTime * attackEventPercentage);
    }

    public override void FrameFeed()
    {
        timeElapse += Time.deltaTime / giant.attackTime;
        CheckState();

        if (stopFrameFeed) return;
        UpdateVisualAttack();
    }

    public override void Notify(EventTrigger nerbyEvent)
    {
        // Can get stun events ecc...
    }
    #endregion

    protected virtual void CheckState()
    {
        if (timeElapse > 1)
        {
            timeElapse = 0f;
            stopFrameFeed = false;

            giant.ChangeState<GiantAlertState>();
            giant.SetSpeed(SpeedLevel.Fast);
        }
    }

    protected virtual void UpdateVisualAttack()
    {
        if (timeElapse > attackEventPercentage)
        {
            stopFrameFeed = true;
            giant.Attack();

            float percentage = 1f - attackEventPercentage;
            giant.RecoverAttack(giant.attackTime * percentage);
        }
    }
}