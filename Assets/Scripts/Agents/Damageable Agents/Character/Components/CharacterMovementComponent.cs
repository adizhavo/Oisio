using UnityEngine;
using Oisio.Agent;

public class CharacterMovementComponent : CharacterComponent
{
    private CharacterStaminaComponent staminaComponent;

    private float staminaCost
    {
        get 
        {
            return agent.staminaCost * Time.deltaTime;
        }
    }

    public bool isRunning
    {
        get
        {
            return InputConfig.Run() && HasEnoughStamina() && isMoving;
        }
    }

    public bool isMoving
    {
        get 
        {
            return Mathf.Abs(InputConfig.XDriection()) > Mathf.Epsilon || Mathf.Abs(InputConfig.YDriection()) > Mathf.Epsilon;
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
        if (InputConfig.Run() && isMoving) staminaComponent.ConsumeStamina(staminaCost);
        float speed = isRunning ? agent.runSpeed : agent.walkSpeed;
        return speed;
    }

    private bool HasEnoughStamina()
    {
        return staminaComponent != null && staminaComponent.stamina > staminaCost;
    }

    private void MoveAgent()
    {
        if (isMoving)
        {
            Vector3 moveDirection = agent.transform.position + new Vector3(InputConfig.XDriection(), 0, InputConfig.YDriection());
            agent.navMeshAgent.SetDestination(moveDirection);
        }
        else
            agent.navMeshAgent.speed = 0f;
    }
}
