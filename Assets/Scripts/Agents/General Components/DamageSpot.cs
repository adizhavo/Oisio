using UnityEngine;

public class DamageSpot : MonoBehaviour, Damageable, EventListener
{
    [SerializeField] private MonsterAgent giant;

    public float DamageResist;

    #region Damagable implementation

    public void ApplyDamage(float damage)
    {
        if (giant == null) return;

        AgentHealth healthComp = giant.RequestComponent<AgentHealth>();
        if (healthComp != null)
        {
            float damageReceived = - (DamageResist - damage);
            healthComp.ApplyDamage(damageReceived);
        }
    }

    #endregion

    #region EventListener implementation

    public void Notify(EventTrigger visibleAction)
    {
        giant.Notify(visibleAction);
    }

    public float VisibilityRadius
    {
        get
        {
            return giant.VisibilityRadius;
        }
    }

    public Vector3 WorlPos { set; get; }

    #endregion
}
