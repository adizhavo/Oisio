using UnityEngine;
using System.Collections;

public class GiantAttackComponent : AgentComponent
{
    private MonsterAgent attacker;

    public GiantAttackComponent(MonsterAgent attacker)
    {
        this.attacker = attacker;
    }

    public void Attack<T>() where T : Agent
    {
        T[] damageables = GameObject.FindObjectsOfType<T>();

        foreach(T d in damageables)
        {
            float distance = Vector3.Distance(attacker.WorlPos, d.WorlPos);
            if (distance > attacker.VisibilityRadius) return;

            AgentHealth health = d.RequestComponent<AgentHealth>();

            if (health != null)
            {
                health.ApplyDamage(attacker.attackDamage);
            }
        }
    }

    #region AgentComponent implementation

    public void FrameFeed() { }

    #endregion
}
