using UnityEngine;
using Oisio.Agent;

namespace Oisio.Agent.State
{
    public class GiantAttackState : MonsterState
    {
        protected float attackEventPercentage = 0.7f;

        protected bool stopFrameFeed = false;
        protected float timeElapse;

        public GiantAttackState(GiantAgent monster) : base(monster) { }

        #region implemented abstract members of GiantActionState
        protected override void Init()
        {
            #if UNITY_EDITOR
            Debug.Log("Giant enters into Attack state..");
            #endif

            attackEventPercentage = Mathf.Clamp01(attackEventPercentage);
            monster.PrepareAttack(monster.attackTime * attackEventPercentage);
        }

        public override void FrameFeed()
        {
            timeElapse += Time.deltaTime / monster.attackTime;
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

                monster.ChangeState<GiantAlertState>();
                monster.SetSpeed(SpeedLevel.Fast);
            }
        }

        protected virtual void UpdateVisualAttack()
        {
            if (timeElapse > attackEventPercentage)
            {
                stopFrameFeed = true;
                monster.Attack();
                CameraShake.Instance.StartShake(ShakeType.GiantAttack);

                float percentage = 1f - attackEventPercentage;
                monster.RecoverAttack(monster.attackTime * percentage);
            }
        }
    }
}