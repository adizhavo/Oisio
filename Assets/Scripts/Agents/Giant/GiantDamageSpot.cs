using UnityEngine;

public class GiantDamageSpot : MonoBehaviour, Damagable 
{
    [SerializeField] private GiantAgent giant;

    public float DamageResist;

    #region Damagable implementation

    public void ApplyDamage(float damage)
    {
        if (giant == null) return;

        GiantHealth healthComp = giant.RequestComponent<GiantHealth>();
        if (healthComp != null)
        {
            float damageReceived = - (DamageResist - damage);
            healthComp.ApplyDamage(damageReceived);
        }
    }

    #endregion
}
