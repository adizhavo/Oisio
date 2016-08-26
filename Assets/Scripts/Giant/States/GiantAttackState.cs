using UnityEngine;
using System.Collections;

public class GiantAttackState : GiantActionState
{
    public float attackEventPercentage = 0.7f;

    private bool stopFrameFeed = false;
    private float timeElapse;

    public GiantAttackState(GiantAgent giant) : base(giant) { }

    #region implemented abstract members of GiantActionState
    public override void Init()
    {
        attackEventPercentage = Mathf.Clamp01(attackEventPercentage);
        giant.SetVisualAttack(attackEventPercentage);

        Debug.Log("ATTACKSTATE");
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

    private void CheckState()
    {
        if (timeElapse > 1)
        {
            timeElapse = 0f;
            stopFrameFeed = false;
            giant.ChangeState<GiantAlertState>();
        }
    }

    private void UpdateVisualAttack()
    {
        if (timeElapse > attackEventPercentage)
        {
            giant.SetVisualAttack(0);
            stopFrameFeed = true;
        }
    }
}