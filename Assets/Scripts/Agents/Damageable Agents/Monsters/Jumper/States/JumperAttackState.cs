using UnityEngine;
using Oisio.Agent;

namespace Oisio.Agent.State
{
    public class JumperAttackState : MonsterState
    {
        private JumperAgent jumper
        {
            get
            {
                return (JumperAgent)monster;
            }
        }

        public JumperAttackState(JumperAgent monster) : base (monster) { }

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
            if (!eventPos.HasValue && nerbyEvent.subject.Equals(EventSubject.Attack))
            {
                eventPos = nerbyEvent.WorlPos;
                startPos = monster.WorlPos;

                float distance = Vector3.Distance(eventPos.Value, startPos);
                jumpTime = distance / jumper.jumpSpeed;
                monster.PrepareAttack(jumpTime);
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
            monster.transform.position = calcPos;
            jumper.jumperRoot.transform.localPosition = new Vector3(0f, monster.WorlPos.y + height, 0f);
        }

        private void TriggerAttack()
        {
            monster.Attack();
            monster.RecoverAttack(0.2f);
            jumper.jumperRoot.transform.localPosition = Vector3.zero;
            monster.ChangeState<JumperAlertState>();
        }
    }
}