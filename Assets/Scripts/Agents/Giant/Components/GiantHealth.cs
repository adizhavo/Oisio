using UnityEngine;
using System.Collections;

public class GiantHealth : AgentComponent
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
            return giant.maxHealth;
        }
    }

    public float healthRegen
    {
        get 
        {
            return giant.healthRegen * Time.deltaTime;
        }
    }

    private GiantAgent giant;

    public GiantHealth(GiantAgent agent)
    {
        this.giant = agent;
        health = giant.maxHealth;
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
        Debug.Log("Damage Applied : " + damage);

        health -= damage;
    }
}
