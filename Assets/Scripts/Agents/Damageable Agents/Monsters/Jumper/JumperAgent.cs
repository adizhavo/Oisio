using Oisio.Game;
using UnityEngine;
using Oisio.Agent.State;
using Oisio.Agent.Component;
using System.Collections.Generic;

namespace Oisio.Agent
{
    [RequireComponent(typeof(Animator))]
    public class JumperAgent : MonsterAgent 
    {
        [Header("Jumper Configuration")]
        public GameObject jumperRoot;
        public float jumpDistance;
        public float heightMultiplier;
        public float jumpSpeed;

        [Header("Jumper FX Configuration")]
        public GameObjectPool AttackEffect;
        public GameObjectPool DeathEffect;
        public GameObject DamageZone;

        #region MonsterAgent implementation

        protected override void Init()
        {
            base.Init();
            ChangeState<JumperIdleState>();
        }

        protected override List<AgentComponent> InitComponents()
        {
            return new List<AgentComponent>
            {
                new AttackAnimation(),
                new MapBlockHolder(),
                new AgentHealth(this, null),
                new MonsterAttackComponent(this)
            };
        }

        protected override AgentState[] InitStates()
        {
            return new AgentState[]
            {
                new JumperIdleState(this),
                new JumperAlertState(this), 
                new JumperAttackState(this), 
                new GiantBlindState(this),
                new AgentDeathState(this, DeathEffect)
            };
        }

        public override void Attack()
        {
            RequestComponent<MonsterAttackComponent>().Attack<CharacterAgent>();
            CameraShake.Instance.StartShake(ShakeType.JumperAttack);
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