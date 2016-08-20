using UnityEngine;

public class Projectile : MonoBehaviour 
{
    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.isKinematic = true;
    }

    public void Shoot(Vector3 force)
    {
        rigidBody.isKinematic = false;
        rigidBody.velocity = force;
    }

    private void Update()
    {
        if (rigidBody.isKinematic) return;

        Vector3 diff = rigidBody.velocity;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    private void OnCollisionEnter(Collision col)
    {
        rigidBody.isKinematic = true;
    }
}