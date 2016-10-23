using UnityEngine;
using Oisio.Game;
using Oisio.Agent.Component;
using System.Collections.Generic;

namespace Oisio.Agent.State
{
    public class CharacterDashState : AgentState
    {
        private Vector3 starPos;
        private Vector3 endPos;
        private CharacterAgent agent;

        private AgentHealth health;

        private float dashTime;
        private float timeCounter;

        private List<AgentComponent> disabledComponents = new List<AgentComponent>();

        public CharacterDashState(CharacterAgent agent)
        {
            this.agent = agent;
        }

        #region AgentState implementation

        public void Init(EventTrigger initialTrigger)
        {
            AdjustComponentsForDashState();
            CalculatePositionsAndCheckPath();

            timeCounter = 0f;
        }

        public void FrameFeed()
        {
            timeCounter += Time.deltaTime / dashTime;
            agent.transform.position = Vector3.Lerp(starPos, endPos, timeCounter);

            if (timeCounter > 1f) RestoreComponentAndState();
        }

        public void Notify(EventTrigger nerbyEvent) { }

        #endregion

        private void AdjustComponentsForDashState()
        {
            disabledComponents.Add(agent.RequestComponent<AgentHealth>());
            disabledComponents.Add(agent.RequestComponent<CharacterDashComponent>());
            disabledComponents.Add(agent.RequestComponent<CharacterAttackComponent>());
            disabledComponents.Add(agent.RequestComponent<CharcterSmokebombComponent>());
            disabledComponents.Add(agent.RequestComponent<CharacterMovementComponent>());

            agent.RemoveComponent<AgentHealth>();
            agent.RemoveComponent<CharacterDashComponent>();
            agent.RemoveComponent<CharacterAttackComponent>();
            agent.RemoveComponent<CharcterSmokebombComponent>();
            agent.RemoveComponent<CharacterMovementComponent>();
        }

        private void CalculatePositionsAndCheckPath()
        {
            starPos = agent.WorlPos;
            Vector3 moveDir = new Vector3(InputConfig.XDirection(), 0, InputConfig.YDirection()).normalized;
            endPos = starPos + moveDir * agent.dashDistance;

            NavMeshHit hit;
            bool blocked = NavMesh.Raycast(agent.WorlPos, endPos, out hit, NavMesh.AllAreas);

            if (blocked)
            {
                endPos = hit.position;
                dashTime = Vector3.Distance(starPos, endPos) / agent.dashSpeed;
            }
            else
                dashTime = agent.dashDistance / agent.dashSpeed;
        }

        private void RestoreComponentAndState()
        {
            agent.navMeshAgent.ResetPath();
            agent.ChangeState<NullAgentState>();

            agent.AddComponents(disabledComponents.ToArray());
            disabledComponents.Clear();
        }
    }
}