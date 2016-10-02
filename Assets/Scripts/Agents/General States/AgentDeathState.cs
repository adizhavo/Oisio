using UnityEngine;
using Oisio.Game;
using System.Collections;

namespace Oisio.Agent.State
{
    public class AgentDeathState : AgentState
    {
        private Agent dyingAgent;
        private GameObjectPool deathParticleFX;

        public AgentDeathState(Agent dyingAgent, GameObjectPool deathParticleFX)
        {
            this.dyingAgent = dyingAgent;
            this.deathParticleFX = deathParticleFX;
        }

        #region AgentState implementation

        public void Init(EventTrigger initialTrigger)
        {
            dyingAgent.RemoveAllComponents();
            dyingAgent.WorlPos = dyingAgent.transform.position;

            PooledObjects.Instance.RequestGameObject(deathParticleFX).transform.position = dyingAgent.WorlPos;
            GameObject.Destroy(dyingAgent.gameObject);
        }

        public void FrameFeed() { }

        public void Notify(EventTrigger nerbyEvent) { }

        #endregion
    }
}