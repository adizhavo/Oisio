using UnityEngine;
using Oisio.Game;
using Oisio.Agent.State;

namespace Oisio.Agent.Component
{
    public class CharacterDashComponent : CharacterComponent
    {
        private CharacterMovementComponent moveComponent;
        private CharacterStaminaComponent staminaComponent;

        public bool CanDash
        {
            get 
            { 
                return staminaComponent != null && staminaComponent.stamina - agent.dashStaminaCost > 0f;
            }
        }

        public CharacterDashComponent(CharacterAgent agent) : base(agent) { }

        #region implemented CharacterComponent

        public override void FrameFeed()
        {
            if (moveComponent == null || staminaComponent == null)
            {
                moveComponent = agent.RequestComponent<CharacterMovementComponent>();
                staminaComponent = agent.RequestComponent<CharacterStaminaComponent>();
                return;
            }

            if (InputConfig.Dash() && CanDash && moveComponent.isMoving)
            {
                staminaComponent.ConsumeStamina(agent.dashStaminaCost);
                agent.ChangeState<CharacterDashState>();
            }
        }
        #endregion
    }
}