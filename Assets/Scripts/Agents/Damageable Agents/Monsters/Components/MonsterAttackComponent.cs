using UnityEngine;
using Oisio.Agent;

namespace Oisio.Agent.Component
{
    public class MonsterAttackComponent : AgentComponent
    {
        private MonsterAgent attacker;

        public MonsterAttackComponent(MonsterAgent attacker)
        {
            this.attacker = attacker;
        }

        public void Attack<T>() where T : Agent
        {
            T[] damageables = GameObject.FindObjectsOfType<T>();

            foreach(T d in damageables)
            {
                float distance = Vector3.Distance(attacker.WorlPos, d.WorlPos);
                if (distance > attacker.attackRange) return;

                AgentHealth health = d.RequestComponent<AgentHealth>();

                if (health != null)
                {
                    health.ApplyDamage(attacker.attackDamage);
                }
            }
        }

        #region AgentComponent implementation

        public void FrameFeed() { }

        #endregion
    }
}