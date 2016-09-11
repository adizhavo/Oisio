using UnityEngine;

public class DamageSpot : MonoBehaviour, Damageable 
{
    [SerializeField] private DamageableAgent agent;

    public float DamageResist;

    #region Damagable implementation

    public void ApplyDamage(float damage)
    {
        if (agent == null) return;

        AgentHealth healthComp = agent.RequestComponent<AgentHealth>();
        if (healthComp != null)
        {
            float damageReceived = - (DamageResist - damage);
            healthComp.ApplyDamage(damageReceived);
        }
    }

    #endregion
}
