using Oisio.Game;
using UnityEngine;
using Oisio.Agent;
using System.Collections;

namespace Oisio.Agent.Component
{
    // handles character run animation and fx
    public class CharacterAnimationComponent : CharacterComponent
    {
        public static readonly string RunKey = "run";
        private CharacterMovementComponent movementComp;

        public CharacterAnimationComponent(CharacterAgent agent) : base (agent) { }

        #region implemented abstract members of AgentComponent

        private bool isRunning
        {
            get 
            {
                if (movementComp == null) movementComp = agent.RequestComponent<CharacterMovementComponent>();
                return movementComp.isRunning;
            }
        }

        private bool isWalking
        {
            get 
            {
                if (movementComp == null) movementComp = agent.RequestComponent<CharacterMovementComponent>();
                return movementComp.isMoving;
            }
        }

        public override void FrameFeed()
        {
            float runningSpeed = isRunning ? agent.runSpeed : agent.walkSpeed;
            SetRun(runningSpeed);
            UpdateCameraShake();
        }

        #endregion

        protected virtual void SetRun(float speed)
        {
            agent.characterAnimator.SetFloat(RunKey, speed);
        }

        private bool currentRunning = false;

        private void UpdateCameraShake()
        {
            if (currentRunning != isRunning)
            {
                currentRunning = isRunning;
                if (currentRunning) CameraShake.Instance.StartShake(ShakeType.Run);
                else CameraShake.Instance.StopShake(ShakeType.Run);
            }

            if (!currentRunning && isWalking) CameraShake.Instance.StartShake(ShakeType.Walk);
            else CameraShake.Instance.StopShake(ShakeType.Walk);
        }
    }
}