using UnityEngine;

public class Projectile : MonoBehaviour 
{
    public Transform Graphic;
    private Rigidbody rigidBody;

    public float Damage;
    private float deviationStrenght = 1;

    private Vector3 deviation;
    private Vector3 currentDirection;

    public EventTrigger shooter;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.isKinematic = true;
    }

    public void Shoot(EventTrigger shooter, Vector3 force)
    {
        Init(shooter, force, force);
    }

    public void Shoot(EventTrigger shooter, Vector3 force, Vector3 deviation)
    {
        Init(shooter, force, deviation * new Vector3(force.x, 0, force.z).magnitude);
    }

    private void Init(EventTrigger shooter, Vector3 force, Vector3 deviation)
    {
        rigidBody.isKinematic = false;
        rigidBody.velocity = force;
        this.shooter = shooter;
        this.deviation = deviation;
    }

    private void Update()
    {
        if (rigidBody.isKinematic) return;

        DeviationCorrection();
        ArrowDirection();
    }

    private void DeviationCorrection()
    {
        rigidBody.velocity = new Vector3(deviation.x, rigidBody.velocity.y, deviation.z) * deviationStrenght;
    }

    private void ArrowDirection()
    {
        float zRot = Mathf.Atan2(rigidBody.velocity.y, rigidBody.velocity.x) * Mathf.Rad2Deg;
        Graphic.rotation = Quaternion.Euler(Graphic.rotation.x, Graphic.rotation.y, zRot - 90);
    }

    private void OnCollisionEnter(Collision col)
    {
        DisableCollisions(col);
        DeliverEvent(col);
        ApplyDamage(col);
    }

    private void DisableCollisions(Collision col)
    {
        rigidBody.isKinematic = true;
        transform.SetParent(col.transform);
        GetComponent<BoxCollider>().enabled = false;
    }

    private void DeliverEvent(Collision col)
    {
        if (shooter == null) return;

        EventListener listener = col.transform.GetComponent<EventListener>();
        if (listener != null)
        {
            listener.Notify(shooter);
        }

        shooter = null;
    }

    private void ApplyDamage(Collision col)
    {
        Damageable dmg = col.transform.GetComponent<Damageable>();
        if (dmg != null)
        {
            dmg.ApplyDamage(Damage);
        }
    }
}