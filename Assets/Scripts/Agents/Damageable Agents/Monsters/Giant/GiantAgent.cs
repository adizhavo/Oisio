using UnityEngine;
using Oisio.Agent.State;
using Oisio.Agent.Component;
using System.Collections.Generic;

namespace Oisio.Agent
{
    [RequireComponent(typeof(Animator))]
    public class GiantAgent : MonsterAgent
    {
        [Header("Giant FX Configuration")]
        public GameObjectPool AttackEffect;
        public GameObject DamageZone;

        #region MonsterAgent implementation

        protected override void Init ()
        {   
            base.Init();
            ChangeState<GiantIdleState>();
    	}

        protected override List<AgentComponent> InitComponents()
        {
            return new List<AgentComponent>
            {
                new AttackAnimation(),
                new MapBlockHolder(),
                new AgentHealth(this),
                new MonsterAttackComponent(this)
            };
        }

        protected override AgentState[] InitStates()
        {
            // all available states will be inserted in this array
            return new MonsterState[]
                {
                    new GiantIdleState(this),
                    new GiantAlertState(this),
                    new GiantAttackState(this),
                    new GiantHuntState(this),
                    new GiantRageState(this), 
                    new GiantBlindState(this)
                    // next state
                    // ...
                };
        }

        public override void Attack()
        {
            RequestComponent<MonsterAttackComponent>().Attack<CharacterAgent>();
            CameraShake.Instance.StartShake(ShakeType.GiantAttack);
            DisplayAttackEffect();
        }

        public override void PrepareAttack(float preparationTime)
        {
            RequestComponent<AttackAnimation>().PrepareAttack(GetComponent<Animator>(), DamageZone, preparationTime);
        }

        public override void RecoverAttack(float recoverTime)
        {
            RequestComponent<AttackAnimation>().Recover(GetComponent<Animator>(), DamageZone, recoverTime);
        }

        #endregion

        private void DisplayAttackEffect()
        {
            PooledObjects.Instance.RequestGameObject(AttackEffect).transform.position = new Vector3(transform.position.x, 0f, transform.position.z);  
        }
    }
}