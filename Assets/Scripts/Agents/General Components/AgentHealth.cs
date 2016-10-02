using UnityEngine;
using Oisio.Agent;
using Oisio.Agent.State;

namespace Oisio.Agent.Component
{
    public class AgentHealth : AgentComponent 
    {
        public float health
        {
            private set;
            get;
        }

        public float maxhealth
        {
            get 
            {
                return agent.maxHealth;
            }
        }

        public float healthRegen
        {
            get 
            {
                return agent.healthRegen * Time.deltaTime;
            }
        }

        private DamageableAgent agent;

        public AgentHealth(DamageableAgent agent)
        {
            this.agent = agent;
            health = agent.maxHealth;
        }

        #region AgentComponent implementation

        public void FrameFeed()
        {
            health += healthRegen;
            health = Mathf.Clamp(health, 0f, maxhealth);
        }

        #endregion

        public void ApplyDamage(float damage)
        {
            health -= damage;
            if (health < 0f) agent.ChangeState<AgentDeathState>();
        }
    }
}