using UnityEngine;
using Oisio.Agent;

public class DamageSpot : MonoBehaviour, Damageable, EventListener
{
    [SerializeField] private MonsterAgent giant;

    public float DamageResist;
    public GameObjectPool HitEffect;

    #region Damagable implementation

    public void ApplyDamage(float damage)
    {
        DamageMonster(damage);
        DisplayHitEffect(null);
    }

    public void ApplyDamage(float damage, Vector3 hitPos)
    {
        DamageMonster(damage);
        Vector3? effectPos = hitPos;
        DisplayHitEffect(effectPos);
    }

    #endregion

    #region EventListener implementation

    private void DamageMonster(float damage)
    {
        if (giant == null) return;

        AgentHealth healthComp = giant.RequestComponent<AgentHealth>();
        if (healthComp != null)
        {
            float damageReceived = - (DamageResist - damage);
            healthComp.ApplyDamage(damageReceived);
        }
    }

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

    private void DisplayHitEffect(Vector3? effectPos)
    {
        if (!effectPos.HasValue) return;

        Transform hitEff = PooledObjects.Instance.RequestGameObject(HitEffect).transform;
        hitEff.transform.position = effectPos.Value;
        hitEff.LookAt(giant.transform.position);
    }
}
