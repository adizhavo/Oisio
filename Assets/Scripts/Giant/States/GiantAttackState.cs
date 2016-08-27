using UnityEngine;
using System.Collections;

public class GiantAttackState : GiantActionState
{
    protected float attackEventPercentage = 0.7f;

    protected bool stopFrameFeed = false;
    protected float timeElapse;

    public GiantAttackState(GiantAgent giant) : base(giant) { }

    #region implemented abstract members of GiantActionState
    public override void Init()
    {
        attackEventPercentage = Mathf.Clamp01(attackEventPercentage);
        giant.Attack(giant.AttackTime * attackEventPercentage);
    }

    public override void FrameFeed()
    {
        timeElapse += Time.deltaTime / giant.AttackTime;
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
        }
    }

    protected virtual] void UpdateVisualAttack()
    {
        if (timeElapse > attackEventPercentage)
        {
            float percentage = 1f - attackEventPercentage;
            giant.RecoverAttack(giant.AttackTime * percentage);
            stopFrameFeed = true;
        }
    }
}