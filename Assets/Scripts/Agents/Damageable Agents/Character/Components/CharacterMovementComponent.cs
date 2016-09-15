using UnityEngine;

public class CharacterMovementComponent : CharacterComponent
{
    private CharacterStaminaComponent staminaComponent;

    private float staminaCost
    {
        get 
        {
            return GameConfig.staminaCost * Time.deltaTime;
        }
    }

    public CharacterMovementComponent(CharacterAgent agent) : base (agent) { }

    #region implemented abstract members of AgentComponent
    public override void FrameFeed()
    {
        if (staminaComponent == null) staminaComponent = agent.RequestComponent<CharacterStaminaComponent>();
        agent.navMeshAgent.speed = AgentSpeed();
        MoveAgent();
    }
    #endregion

    private float AgentSpeed()
    {
        bool isMoving = Mathf.Abs(InputConfig.XDriection()) + Mathf.Abs(InputConfig.YDriection()) > Mathf.Epsilon;
        bool isRunning = InputConfig.Run() && HasEnoughStamina() && isMoving;

        if (InputConfig.Run() && isMoving) staminaComponent.ConsumeStamina(staminaCost);
        float speed = isRunning ? GameConfig.maxCharacterSpeed : GameConfig.minCharacterSpeed;
        return speed;
    }

    private bool HasEnoughStamina()
    {
        return staminaComponent != null && staminaComponent.stamina > staminaCost;
    }

    private void MoveAgent()
    {
        Vector3 moveDirection = agent.transform.position + new Vector3(InputConfig.XDriection(), 0, InputConfig.YDriection());
        agent.navMeshAgent.SetDestination(moveDirection);
    }
}
