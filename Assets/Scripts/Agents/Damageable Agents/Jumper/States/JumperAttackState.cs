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
    private float targetAngle = 180f;
    private Vector3 startPos;

    #region JunperAttackState implementation

    protected override void Init()
    {
        #if UNITY_EDITOR
        Debug.Log("Jumper enters into Attack state..");
        #endif

//        giant.Stop();
        giant.PrepareAttack(jumper.jumpTime);
        startPos = giant.WorlPos;

        angle = 0f;
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
        }
    }

    #endregion

    private void Jump()
    {
        if (eventPos.HasValue)
        {
            angle += (2 * Time.deltaTime) / jumper.jumpTime ;
            float sinValue = Mathf.Sin(angle);
            float height = jumper.jumpHeight * sinValue;

            Vector3 targetPos = Vector3.ClampMagnitude(eventPos.Value - startPos, jumper.jumpDistance);
            Vector3 calcPos = Vector3.Lerp(startPos, startPos + targetPos, angle / (targetAngle * Mathf.Deg2Rad) );
            giant.transform.position = calcPos;
            jumper.jumperRoot.transform.localPosition = new Vector3(0f, giant.WorlPos.y + height, 0f);
        }

        if (angle >= targetAngle * Mathf.Deg2Rad)
        {
            giant.Attack();
            giant.RecoverAttack(0.2f);

            giant.ChangeState<JumperAlertState>();
        }
    }
}
