using UnityEngine;

public class Projectile : MonoBehaviour 
{
    public Transform Graphic;

    private Rigidbody rigidBody;
    private Vector3 deviation;
    private float deviationStrenght = 1;

    private Vector3 currentDirection;

    public EventTrigger shooter;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.isKinematic = true;
    }

    public void Shoot(EventTrigger shooter, Vector3 force)
    {
        rigidBody.isKinematic = false;
        rigidBody.velocity = force;
        this.deviation = force;
        this.shooter = shooter;
    }

    public void Shoot(EventTrigger shooter, Vector3 force, Vector3 deviation)
    {
        rigidBody.isKinematic = false;
        rigidBody.velocity = force;
        this.shooter = shooter;

        this.deviation = deviation * new Vector3(force.x, 0, force.z).magnitude;
    }

    private void Update()
    {
        if (rigidBody.isKinematic) return;

        DeviationCorrection();
        ArrowDirection();
    }

    private void DeviationCorrection()
    {
        rigidBody.velocity = new Vector3(deviation.x, rigidBody.velocity.y, deviation.z);
    }

    private void ArrowDirection()
    {
        float zRot = Mathf.Atan2(rigidBody.velocity.y, rigidBody.velocity.x) * Mathf.Rad2Deg;
        Graphic.rotation = Quaternion.Euler(Graphic.rotation.x, Graphic.rotation.y, zRot - 90);
    }

    private void OnCollisionEnter(Collision col)
    {
        rigidBody.isKinematic = true;
        transform.SetParent(col.transform);
        GetComponent<BoxCollider>().enabled = false;

        if (shooter == null) return;

        EventListener listener = col.transform.GetComponent<EventListener>();
        if (listener != null)
        {
            listener.Notify(shooter);
        }

        shooter = null;
    }
}