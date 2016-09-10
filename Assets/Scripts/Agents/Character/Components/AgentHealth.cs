using UnityEngine;
using System.Collections;

public class AgentHealth : AgentComponent 
{
    public float health
    {
        private set;
        get;
    }

    public float maxhealth
    {
        get 
        {
            return agent.maxHealth;
        }
    }

    public float healthRegen
    {
        get 
        {
            return agent.healthRegen * Time.deltaTime;
        }
    }

    private CharacterAgent agent;

    public AgentHealth(CharacterAgent agent)
    {
        this.agent = agent;
        health = agent.maxHealth;
    }

    #region AgentComponent implementation

    public void FrameFeed()
    {
        health += healthRegen;
        health = Mathf.Clamp(health, 0f, maxhealth);
    }

    #endregion

    public void ApplyDamage(float damage)
    {
        Debug.Log("Agent damaged : " + damage);

        health -= damage;
    }
}
