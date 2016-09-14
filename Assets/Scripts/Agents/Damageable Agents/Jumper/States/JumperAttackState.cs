using UnityEngine;
using System.Collections;

public class JumperAttackState : GiantState
{
    private JumperAgent jumper;

    public JumperAttackState(JumperAgent giant) : base (giant)
    {
        jumper = giant;
    }

    private Vector3? eventPos = null;

    private float angle;
    private float jumpTime;
    private Vector3 startPos;
    private float targetAngle = 180f;

    #region JunperAttackState implementation

    protected override void Init()
    {
        #if UNITY_EDITOR
        Debug.Log("Jumper enters into Attack state..");
        #endif

        giant.PrepareAttack(jumper.jumpSpeed);
        startPos = giant.WorlPos;

        angle = 0f;
        jumpTime = 0f;
        eventPos = null;
    }

    public override void FrameFeed()
    {
        Jump();
    }

    public override void Notify(EventTrigger nerbyEvent)
    {
        if (nerbyEvent.subject.Equals(EventSubject.Attack))
        {
            eventPos = nerbyEvent.WorlPos;

            float distance = Vector3.Distance(eventPos.Value, startPos);
            jumpTime = distance / jumper.jumpSpeed;
        }
    }

    #endregion

    private void Jump()
    {
        if (eventPos.HasValue)
        {
            CalculateJump();
        }

        if (angle >= targetAngle * Mathf.Deg2Rad)
        {
            TriggerAttack();
        }
    }

    private void CalculateJump()
    {
        angle += (2 * Time.deltaTime) / jumpTime;
        float sinValue = Mathf.Sin(angle);
        Vector3 targetPos = Vector3.ClampMagnitude(eventPos.Value - startPos, jumper.jumpDistance);

        float distance = Vector3.Distance(eventPos.Value, startPos);
        float height = jumper.heightMultiplier * sinValue * distance / 2;

        Vector3 calcPos = Vector3.Lerp(startPos, startPos + targetPos, angle / (targetAngle * Mathf.Deg2Rad) );
        giant.transform.position = calcPos;
        jumper.jumperRoot.transform.localPosition = new Vector3(0f, giant.WorlPos.y + height, 0f);
    }

    private void TriggerAttack()
    {
        giant.Attack();
        giant.RecoverAttack(0.2f);
        jumper.jumperRoot.transform.localPosition = Vector3.zero;
        giant.ChangeState<JumperAlertState>();
    }
}
