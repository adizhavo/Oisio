using UnityEngine;

public class AttackAimer : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowParent;
    public bool InvertAiming;
    public float Sensibility = 1f;
    public float ShootForce;

    private Projectile arrowInstance;

    public void Aim()
    {
        CheckArrow();
        RotateAimer();
    }

    private void CheckArrow()
    {
        if (!arrowInstance)
        {
            arrowInstance = (Instantiate(arrowPrefab) as GameObject).GetComponent<Projectile>();
            arrowInstance.transform.SetParent(arrowParent, false);
            arrowInstance.transform.localPosition = Vector3.zero;
        }

        arrowInstance.gameObject.SetActive(true);
    }

    private void RotateAimer()
    {
        float cursorDeltaX = InputConfig.GetCursorMovement().x * AimingDirection() * Sensibility;
        transform.rotation = Quaternion.Euler(0f, 0f, transform.localEulerAngles.z + cursorDeltaX);
    }

    public void ResetAim()
    {
        if (arrowInstance) arrowInstance.gameObject.SetActive(false);
        transform.localEulerAngles = Vector3.zero;
    }

    private int AimingDirection()
    {
        return InvertAiming ? 1 : -1;
    }

    public void ThrowArrow()
    {
        if (!arrowInstance) return;

        Vector3 shootDirection = arrowParent.position - transform.position;
        arrowInstance.Shoot(shootDirection * ShootForce);

        arrowInstance.transform.SetParent(null);
        arrowInstance = null;
    }
}