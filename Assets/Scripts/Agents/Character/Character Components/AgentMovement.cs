using UnityEngine;

public class AgentMovement : CharacterComponent
{
    private AgentStamina staminaComponent;

    private float staminaCost
    {
        get 
        {
            return GameConfig.staminaCost * Time.deltaTime;
        }
    }

    public AgentMovement(CharacterAgent agent) : base (agent) { }

    #region implemented abstract members of AgentComponent
    public override void FrameFeed()
    {
        if (staminaComponent == null) staminaComponent = agent.RequestComponent<AgentStamina>();
        agent.navMeshAgent.speed = AgentSpeed();
        MoveAgent();
    }
    #endregion

    private float AgentSpeed()
    {
        bool isRunning = InputConfig.Run() && HasEnoughStamina();
        float speed = isRunning ? GameConfig.maxCharacterSpeed : GameConfig.minCharacterSpeed;

        if (InputConfig.Run())
        {
            staminaComponent.ConsumeStamina(staminaCost);
        }

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
