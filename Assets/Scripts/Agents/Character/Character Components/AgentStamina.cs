using UnityEngine;

public class AgentStamina : CharacterComponent
{
    public float stamina
    {
        private set;
        get;
    }

    private float staminaRegeneration
    {
        get 
        {
            return agent.staminaRegen * Time.deltaTime;
        }
    }

    public AgentStamina(CharacterAgent agent) : base (agent) 
    {
        stamina = agent.maxStamina;
    }

    #region implemented abstract members of AgentComponent

    public override void FrameFeed()
    {
        stamina += staminaRegeneration;
        stamina = Mathf.Clamp(stamina, 0, agent.maxStamina);
    }

    #endregion

    public void ConsumeStamina(float amount)
    {
        stamina -= amount + staminaRegeneration;
        if (stamina < 0) stamina = 0;
    }
}