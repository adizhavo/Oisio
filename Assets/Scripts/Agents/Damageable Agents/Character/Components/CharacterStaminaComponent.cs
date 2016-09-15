using UnityEngine;

public class CharacterStaminaComponent : CharacterComponent
{
    public float stamina
    {
        private set;
        get;
    }

    public float maxStamina
    {
        get 
        {
            return agent.maxStamina;
        }
    }

    public float staminaRegeneration
    {
        get 
        {
            return agent.staminaRegen * Time.deltaTime;
        }
    }

    private bool consuming = false;

    public CharacterStaminaComponent(CharacterAgent agent) : base (agent) 
    {
        stamina = agent.maxStamina;
    }

    #region implemented abstract members of AgentComponent

    public override void FrameFeed()
    {
        if (consuming)
        {
            consuming = false;
            return;
        }

        stamina += staminaRegeneration;
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
    }

    #endregion

    public void ConsumeStamina(float amount)
    {
        stamina -= amount;
        if (stamina < 0) stamina = 0;

        consuming = true;
    }
}