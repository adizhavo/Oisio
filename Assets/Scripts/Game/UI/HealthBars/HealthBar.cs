using UnityEngine;
using Oisio.Agent;
using Oisio.Agent.Component;

namespace Oisio.Game
{
    public class HealthBar : MonoBehaviour 
    {
        [SerializeField] private DamageableAgent observedAgent;
        private AgentHealth healthComponent;
        protected float percentage = 0f;

        private void Start () 
        {
            healthComponent = observedAgent.RequestComponent<AgentHealth>();
        }

        protected virtual void Update () 
        {
            if (healthComponent == null) return;
            percentage = healthComponent.health / healthComponent.maxhealth;
        }
    }
}